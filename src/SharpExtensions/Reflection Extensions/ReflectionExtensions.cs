using System;
using System.Linq;
using System.Reflection;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extensions for dealing with reflection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines if the given type is assignable to the given generic type.
        /// </summary>
        /// <param name="givenType">Type of the object to test</param>
        /// <param name="genericType">Type of the object to test against</param>
        /// <returns>Result of comparison</returns>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetTypeInfo().ImplementedInterfaces;
            if (interfaceTypes.Any(it => it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == genericType))
                return true;

            if (givenType.GetTypeInfo().IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            var baseType = givenType.GetTypeInfo().BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }

        /// <summary>
        /// Determines if the given type can be assigned from the given generic type
        /// </summary>
        /// <param name="genericType">Type of the object to test</param>
        /// <param name="givenType">Type of the object to test against</param>
        /// <returns>Result of comparison</returns>
        public static bool IsAssignableFromGenericType(this Type genericType, Type givenType) => IsAssignableToGenericType(givenType, genericType);
    }
}