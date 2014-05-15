using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class TempTestClass
    {
        [Test]
        [SetCulture("en-CA")]
        [SetUICulture("en-CA")]
        public void Test1()
        {
            var deserializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.None,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            var json = "{\"inception_date\": \"0001-01-01T00:00:00+00:00\"}";
            var parsedObj = JsonConvert.DeserializeObject<TestClass>(json, deserializerSettings);
            Console.WriteLine(parsedObj.inception_date);

            deserializerSettings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset
            };
            parsedObj = JsonConvert.DeserializeObject<TestClass>(json, deserializerSettings);
            Console.WriteLine(parsedObj.inception_date);

            parsedObj = JsonConvert.DeserializeObject<TestClass>(json);
            Console.WriteLine(parsedObj.inception_date);
        }
    }

    public class TestClass
    {
        public DateTimeOffset inception_date { get; set; }
    }
}
