/*
Copyright (c) 2013, Groupon, Inc.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.

Neither the name of GROUPON nor the names of its contributors may be
used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading;

namespace LocalityUUID
{
    public class UUID
    {
        public static readonly int Pid = ProcessId();

        public static readonly byte[] Mac = MacAddress();

        private const int MaxPid = 65536;

        private const int Increment = 198491317;

        private const char Version = 'b';

        private static readonly int VersionDec = MapToByte(Version, '0');

        private static readonly AtomicInteger COUNTER = new AtomicInteger(
            new Random(DateTime.UtcNow.Millisecond).Next());

        private static readonly char[] Hex =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e',
                'f'
            };

        private static bool sequential;

        protected readonly byte[] Content;

        /**
         * Constructor that generates a new vB UUID using the current process id, MAC address, and timestamp.
         */
        public UUID()
        {
            long time = DateTime.UtcNow.Ticks;
            Content = new byte[16];

            if (!sequential)
            {
                // atomically add a large prime number to the count and get the previous value
                int count = COUNTER.addAndGet(Increment);

                // switch the order of the count in 4 bit segments and place into content
                Content[0] = (byte)(((count & 0xF) << 4) | ((count & 0xF0) >> 4));
                Content[1] = (byte)(((count & 0xF00) >> 4) | ((count & 0xF000) >> 12));
                Content[2] = (byte)(((count & 0xF0000) >> 12) | ((count & 0xF00000) >> 20));
                Content[3] = (byte)(((count & 0xF000000) >> 20) | ((count & 0xF0000000) >> 28));
            }
            else
            {
                int count = COUNTER.addAndGet(1);

                // get the count in order and place into content
                Content[0] = (byte)(count >> 24);
                Content[1] = (byte)(count >> 16);
                Content[2] = (byte)(count >> 8);
                Content[3] = (byte)(count);
            }

            // copy pid to content
            Content[4] = (byte)(Pid >> 8);
            Content[5] = (byte)(Pid);

            // place UUID version (hex 'b') in first four bits and piece of mac address in
            // the second four bits
            Content[6] = (byte)(VersionDec | (0xF & Mac[2]));

            // copy rest of mac address into content
            Content[7] = Mac[3];
            Content[8] = Mac[4];
            Content[9] = Mac[5];

            // copy timestamp into content
            Content[10] = (byte)(time >> 40);
            Content[11] = (byte)(time >> 32);
            Content[12] = (byte)(time >> 24);
            Content[13] = (byte)(time >> 16);
            Content[14] = (byte)(time >> 8);
            Content[15] = (byte)(time);
        }

        /**
         * Constructor that takes a byte array as this UUID's content. This is equivalent to the binary representation
         * of the UUID. Note that the byte array is copied, so if the argument value changes then the constructed UUID
         * will not. This throws an IllegalArgumentException if the byte array is null or not 16 bytes long.
         * @param bytes UUID content as bytes.
         */
        public UUID(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentException("Tried to construct UUID with null byte array");

            if (bytes.Length != 16)
                throw new ArgumentException("Attempted to parse malformed UUID: " + string.Join(",", bytes));

            Array.Copy(bytes, Content, 16);
        }

        /**
         * Constructor that takes a UUID string representation and parses it. This constructor expects the canonical UUID
         * String format validated by the isValidUUID() method and thros an IllegalArgumentException otherwise.
         * @param id UUID String representation, expected to be the valid UUID format.
         */
        public UUID(string id)
        {
            if (id == null)
                throw new ArgumentException("Tried to construct UUID from null String");

            id = id.Trim();
            char[] chars = id.ToCharArray();

            if (!isValidUUID(chars))
                throw new ArgumentException("Attempted to parse malformed UUID: " + id);

            Content = new byte[16];
            Content[0] = MapToByte(chars[0], chars[1]);
            Content[1] = MapToByte(chars[2], chars[3]);
            Content[2] = MapToByte(chars[4], chars[5]);
            Content[3] = MapToByte(chars[6], chars[7]);
            Content[4] = MapToByte(chars[9], chars[10]);
            Content[5] = MapToByte(chars[11], chars[12]);
            Content[6] = MapToByte(chars[14], chars[15]);
            Content[7] = MapToByte(chars[16], chars[17]);
            Content[8] = MapToByte(chars[19], chars[20]);
            Content[9] = MapToByte(chars[21], chars[22]);
            Content[10] = MapToByte(chars[24], chars[25]);
            Content[11] = MapToByte(chars[26], chars[27]);
            Content[12] = MapToByte(chars[28], chars[29]);
            Content[13] = MapToByte(chars[30], chars[31]);
            Content[14] = MapToByte(chars[32], chars[33]);
            Content[15] = MapToByte(chars[34], chars[35]);
        }

        /**
         * Constructs a Locality UUID from a standard Java java.util.UUID object. A java.util.UUID object won't return
         * its content as a byte array, but will return the first and second halves of content as longs. We use these
         * to construct a new Locality UUID object. This throws an IllegalArgumentException if the uuid argument is null.
         * @param uuid A non-null java.util.UUID object, from which we will construct a locality UUID with identical content.
         */
        public UUID(UUID uuid)
        {
            if (uuid == null)
                throw new ArgumentException("Tried to construct Locality UUID with null java.util.UUID");

            Content = new byte[16];
            ConstructFromLongs(uuid.GetMostSignificantBits(), uuid.GetLeastSignificantBits());
        }

        /**
         * Constructs a UUID using long values representing the first and second half of the UUID content. This was added
         * to be consistent with the java.util.UUID constructor.
         * @param mostSigBits Long value representing the first half of the UUID.
         * @param leastSigBits Long value representing the second half of the UUID.
         */
        public UUID(long mostSigBits, long leastSigBits)
        {
            Content = new byte[16];
            ConstructFromLongs(mostSigBits, leastSigBits);
        }

        /**
         * This method sets the content based on the values of two longs representing the first and second half of the UUID.
         * This method is called from the UUID(java.util.UUID) and UUID(long, long) constructors.
         * @param hi Long value representing the first half of the UUID.
         * @param lo Long value representing the second half of the UUID.
         */
        private void ConstructFromLongs(long hi, long lo)
        {
            Content[0] = (byte)(hi >> 56);
            Content[1] = (byte)(hi >> 48);
            Content[2] = (byte)(hi >> 40);
            Content[3] = (byte)(hi >> 32);
            Content[4] = (byte)(hi >> 24);
            Content[5] = (byte)(hi >> 16);
            Content[6] = (byte)(hi >> 8);
            Content[7] = (byte)(hi);
            Content[8] = (byte)(lo >> 56);
            Content[9] = (byte)(lo >> 48);
            Content[10] = (byte)(lo >> 40);
            Content[11] = (byte)(lo >> 32);
            Content[12] = (byte)(lo >> 24);
            Content[13] = (byte)(lo >> 16);
            Content[14] = (byte)(lo >> 8);
            Content[15] = (byte)(lo);
        }

        /**
         * This method validates a UUID String by making sure its non-null and calling isValidUUID(char[]).
         * @param id UUID String.
         * @return True or false based on whether the String can be used to construct a UUID.
         */
        public static bool isValidUUID(String id)
        {
            return id != null && isValidUUID(id.ToCharArray());
        }

        /**
         * This method validates a character array as the expected format for a printed representation of a UUID. The
         * expected format is 36 characters long, with '-' at the 8th, 13th, 18th, and 23rd characters. The remaining
         * characters are expected to be valid hex, meaning in the range ('0' - '9', 'a' - 'f', 'A' - 'F') inclusive.
         * If a character array is valid, then it can be used to construct a UUID. This method has been written unrolled
         * and verbosely, with the theory that this is simpler and faster than using loops or a regex.
         * @param ch A character array of a UUID's printed representation.
         * @return True or false based on whether the UUID is valid, no exceptions are thrown.
         */
        public static bool isValidUUID(char[] ch)
        {
            return ch != null &&
                    ch.Length == 36 &&
                    validHex(ch[0]) &&
                    validHex(ch[1]) &&
                    validHex(ch[2]) &&
                    validHex(ch[3]) &&
                    validHex(ch[4]) &&
                    validHex(ch[5]) &&
                    validHex(ch[6]) &&
                    validHex(ch[7]) &&
                    ch[8] == '-' &&
                    validHex(ch[9]) &&
                    validHex(ch[10]) &&
                    validHex(ch[11]) &&
                    validHex(ch[12]) &&
                    ch[13] == '-' &&
                    validHex(ch[14]) &&
                    validHex(ch[15]) &&
                    validHex(ch[16]) &&
                    validHex(ch[17]) &&
                    ch[18] == '-' &&
                    validHex(ch[19]) &&
                    validHex(ch[20]) &&
                    validHex(ch[21]) &&
                    validHex(ch[22]) &&
                    ch[23] == '-' &&
                    validHex(ch[24]) &&
                    validHex(ch[25]) &&
                    validHex(ch[26]) &&
                    validHex(ch[27]) &&
                    validHex(ch[28]) &&
                    validHex(ch[29]) &&
                    validHex(ch[30]) &&
                    validHex(ch[31]) &&
                    validHex(ch[32]) &&
                    validHex(ch[33]) &&
                    validHex(ch[34]) &&
                    validHex(ch[35]);
        }

        /**
         * Just a simple method to determine if a character is valid hex in the range ('0' - '9', 'a' - 'f', 'A' - 'F').
         * @param c A character to test.
         * @return True or false based on whether or not the character is in the expected range.
         */
        private static bool validHex(char c)
        {
            return (c >= '0' && c <= '9') ||
                    (c >= 'a' && c <= 'f') ||
                    (c >= 'A' && c <= 'F');
        }

        /**
         * Toggle UUID generator into sequential mode, so the random segment is in order and increases by one. In
         * sequential mode, there is presumably a desire that UUIDs generated around the same time should begin with similar
         * characters, but this is difficult in a distributed environment. The solution is to set the counter value based
         * on a hash of the UTC date and time up to a 10 minute precision. This means that UUID classes initialized at
         * similar times should start with similar counter values, but this is not guaranteed. If one of these classes
         * is generating vastly more UUIDs than others, then these counters can become skewed.
         *
         * Calling this method more than once without toggling back to variable mode has no effect, so it probably makes
         * more sense to call this from a static context, like your main method or in a class' static initialization.
         */
        public static void UseSequentialIds()
        {
            if (!sequential)
            {
                // get string that changes every 10 minutes
                const string dateFormatString = "yyyyMMddHHmm";
                string date = DateTime.UtcNow.ToString(dateFormatString).Substring(0, 11);

                // run an md5 hash of the string, no reason this needs to be secure
                byte[] digest;
                try
                {
                    HashAlgorithm md = HashAlgorithm.Create("MD5");
                    digest = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(date));
                }
                catch (Exception e)
                {
                    throw new Exception("Could not create hash of date for the sequential counter", e);
                }

                // create integer from first 4 bytes of md5 hash
                int x;
                x = ((int)digest[0] & 0xFF);
                x |= ((int)digest[1] & 0xFF) << 8;
                x |= ((int)digest[2] & 0xFF) << 16;
                x |= ((int)digest[3] & 0xFF) << 24;
                COUNTER.set(x);
            }
            sequential = true;
        }

        /**
         * Toggle uuid generator into variable mode, so the random segment is in reverse order and
         * increases by a large increment. This is the default mode.
         */
        public static void UseVariableIds()
        {
            sequential = false;
        }

        /**
         * This method maps a hex character to its 4-bit representation in an int.
         * @param x Hex character in the range ('0' - '9', 'a' - 'f', 'A' - 'F').
         * @return 4-bit number in int representing hex offset from 0.
         */
        private static int IntValue(char x)
        {
            if (x >= '0' && x <= '9')
                return x - '0';
            if (x >= 'a' && x <= 'f')
                return x - 'a' + 10;
            if (x >= 'A' && x <= 'F')
                return x - 'A' + 10;
            throw new Exception("Error parsing UUID at character: " + x);
        }

        /**
         * Map two hex characters to 4-bit numbers and combine them to produce 8-bit number in byte.
         * @param a First hex character.
         * @param b Second hex character.
         * @return Byte representation of given hex characters.
         */
        private static byte MapToByte(char a, char b)
        {
            int ai = IntValue(a);
            int bi = IntValue(b);
            return (byte)((ai << 4) | bi);
        }

        /**
         * Get contents of this UUID as a byte array. The array is copied before returning so that it can't be changed.
         * @return Raw byte array of UUID contents.
         */
        public byte[] GetBytes()
        {
            return Content;
        }

        /**
         * Get this UUID object as a String. It will be returned as a canonical 36-character UUID string with lower-case
         * hex characters.
         * @return UUID as String.
         */
        public override string ToString()
        {
            char[] id = new char[36];

            // split each byte into 4 bit numbers and map to hex characters
            id[0] = Hex[(Content[0] & 0xF0) >> 4];
            id[1] = Hex[(Content[0] & 0x0F)];
            id[2] = Hex[(Content[1] & 0xF0) >> 4];
            id[3] = Hex[(Content[1] & 0x0F)];
            id[4] = Hex[(Content[2] & 0xF0) >> 4];
            id[5] = Hex[(Content[2] & 0x0F)];
            id[6] = Hex[(Content[3] & 0xF0) >> 4];
            id[7] = Hex[(Content[3] & 0x0F)];
            id[8] = '-';
            id[9] = Hex[(Content[4] & 0xF0) >> 4];
            id[10] = Hex[(Content[4] & 0x0F)];
            id[11] = Hex[(Content[5] & 0xF0) >> 4];
            id[12] = Hex[(Content[5] & 0x0F)];
            id[13] = '-';
            id[14] = Hex[(Content[6] & 0xF0) >> 4];
            id[15] = Hex[(Content[6] & 0x0F)];
            id[16] = Hex[(Content[7] & 0xF0) >> 4];
            id[17] = Hex[(Content[7] & 0x0F)];
            id[18] = '-';
            id[19] = Hex[(Content[8] & 0xF0) >> 4];
            id[20] = Hex[(Content[8] & 0x0F)];
            id[21] = Hex[(Content[9] & 0xF0) >> 4];
            id[22] = Hex[(Content[9] & 0x0F)];
            id[23] = '-';
            id[24] = Hex[(Content[10] & 0xF0) >> 4];
            id[25] = Hex[(Content[10] & 0x0F)];
            id[26] = Hex[(Content[11] & 0xF0) >> 4];
            id[27] = Hex[(Content[11] & 0x0F)];
            id[28] = Hex[(Content[12] & 0xF0) >> 4];
            id[29] = Hex[(Content[12] & 0x0F)];
            id[30] = Hex[(Content[13] & 0xF0) >> 4];
            id[31] = Hex[(Content[13] & 0x0F)];
            id[32] = Hex[(Content[14] & 0xF0) >> 4];
            id[33] = Hex[(Content[14] & 0x0F)];
            id[34] = Hex[(Content[15] & 0xF0) >> 4];
            id[35] = Hex[(Content[15] & 0x0F)];

            return new String(id);
        }

        /**
         * Get the most significant bits (the first half) of the UUID content as a 64-bit long.
         * @return The first half of the UUID as a long.
         */
        public long GetMostSignificantBits()
        {
            long a;
            a = ((long)Content[0] & 0xFF) << 56;
            a |= ((long)Content[1] & 0xFF) << 48;
            a |= ((long)Content[2] & 0xFF) << 40;
            a |= ((long)Content[3] & 0xFF) << 32;
            a |= ((long)Content[4] & 0xFF) << 24;
            a |= ((long)Content[5] & 0xFF) << 16;
            a |= ((long)Content[6] & 0xFF) << 8;
            a |= ((long)Content[7] & 0xFF);
            return a;
        }

        /**
         * Get the least significant bits (the second half) of the UUID content as a 64-bit long.
         * @return The second half of the UUID as a long.
         */
        public long GetLeastSignificantBits()
        {
            long b;
            b = ((long)Content[8] & 0xFF) << 56;
            b |= ((long)Content[9] & 0xFF) << 48;
            b |= ((long)Content[10] & 0xFF) << 40;
            b |= ((long)Content[11] & 0xFF) << 32;
            b |= ((long)Content[12] & 0xFF) << 24;
            b |= ((long)Content[13] & 0xFF) << 16;
            b |= ((long)Content[14] & 0xFF) << 8;
            b |= ((long)Content[15] & 0xFF);
            return b;
        }

        /**
         * Get the java.util.UUID representation of this UUID object. The java.util.UUID is constructed using the most and
         * least significant bits of this UUID's content. No memory is shared with the new java.util.UUID.
         * @return This com.groupon.uuid.UUID's representation as a java.util.UUID.
         */
        public UUID toNetUUID()
        {
            return new UUID(GetMostSignificantBits(), GetLeastSignificantBits());
        }

        /**
         * Extract version field as a hex char from raw UUID bytes. By default, generated UUIDs will have 'b' as the
         * version, but it is possible to parse UUIDs of different types, '4' for example.
         * @return UUID version as a char.
         */
        public char GetVersion()
        {
            return Hex[(Content[6] & 0xF0) >> 4];
        }

        /**
         * Extract process id from raw UUID bytes and return as int. This only applies for this type of UUID, for other
         * UUID types, such as the randomly generated v4, its not possible to discover process id, so -1 is returned.
         * @return Id of process that generated the UUID, or -1 for unrecognized format.
         */
        public int GetProcessId()
        {
            if (GetVersion() != Version)
                return -1;

            return ((Content[4] & 0xFF) << 8) | (Content[5] & 0xFF);
        }

        /**
         * Extract timestamp from raw UUID bytes and return as int. If the UUID is not the default type, then we can't parse
         * the timestamp out and null is returned.
         * @return Millisecond UTC timestamp from generation of the UUID, or null for unrecognized format.
         */
        public DateTime? GetTimestamp()
        {
            if (GetVersion() != Version)
                return null;

            long time;
            time = ((long)Content[10] & 0xFF) << 40;
            time |= ((long)Content[11] & 0xFF) << 32;
            time |= ((long)Content[12] & 0xFF) << 24;
            time |= ((long)Content[13] & 0xFF) << 16;
            time |= ((long)Content[14] & 0xFF) << 8;
            time |= ((long)Content[15] & 0xFF);
            return new DateTime(time);
        }

        /**
         * Extract MAC address fragment from raw UUID bytes, setting missing values to 0, thus the first 2 and a half bytes
         * will be 0, followed by 3 and a half bytes of the active MAC address when the UUID was generated.
         * @return Byte array of UUID fragment, or null for unrecognized format.
         */
        public byte[] GetMacFragment()
        {
            if (GetVersion() != 'b')
                return null;

            byte[] x = new byte[6];

            x[0] = 0;
            x[1] = 0;
            x[2] = (byte)(Content[6] & 0xF);
            x[3] = Content[7];
            x[4] = Content[8];
            x[5] = Content[9];

            return x;
        }

        /**
         * Basic implementation of equals that checks if the given object is null or a different type, then compares the
         * byte arrays which store the content of the UUID. I've considered making this compatible with the content of
         * java.util.UUID, but not sure thats a good idea given that those are objects of a different type, and doing
         * a deep comparison might be surprising functionality.
         * @param o Object against which we compare this UUID.
         * @return True or false if this UUID's content is identical to the given UUID.
         */
        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o == null || GetType() != o.GetType())
                return false;

            UUID that = (UUID)o;

            if (this.Content.Length != that.Content.Length)
                return false;

            for (int i = 0; i < this.Content.Length; i++)
                if (this.Content[i] != that.Content[i])
                    return false;

            return true;
        }

        /**
         * The hash code implementation just calls Arrays.hashCode() on the contents of this UUID (stored as a byte array).
         * @return The hash value of this object.
         */
        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        /**
         * Get the active MAC address on the current machine as a byte array. This is called when generating a new UUID.
         * Note that a machine can have multiple or no active MAC addresses. This method works by iterating through the list
         * of network interfaces, ignoring the loopback interface and any virtual interfaces (which often have made-up
         * addresses), and returning the first one we find. If no valid addresses are found, then a byte array of the same
         * length with all zeros is returned.
         * @return 6-byte array for first active MAC address, or 6-byte zeroed array if no interfaces are active.
         */
        private static byte[] MacAddress()
        {
            try
            {
                IEnumerable<NetworkInterface> interfaces = NetworkInterface.GetAllNetworkInterfaces();
                byte[] mac = null;

                /// TODO: Check if there are other types we need to filter out.
                foreach (var netInterface in interfaces.Where(netInterface => netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                {
                    mac = netInterface.GetPhysicalAddress().GetAddressBytes();
                }

                // if the machine is not connected to a network it has no active MAC address
                return mac ?? (new byte[] { 0, 0, 0, 0, 0, 0 });
            }
            catch (Exception e)
            {
                throw new Exception("Could not get MAC address");
            }
        }

        /**
         * Get the process id of this JVM. I haven't tested this extensively so its possible that this performs differently
         * on esoteric JVMs. I copied this from:
         * http://stackoverflow.com/questions/35842/how-can-a-java-program-get-its-own-process-id
         * @return Id of the current JVM process.
         */
        private static int ProcessId()
        {
            return Process.GetCurrentProcess().Id % MaxPid;
        }
    }
}
