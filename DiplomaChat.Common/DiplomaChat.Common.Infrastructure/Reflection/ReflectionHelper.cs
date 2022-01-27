using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiplomaChat.Common.Infrastructure.Reflection
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute<TAttribute>(Assembly executingAssembly)
            where TAttribute : Attribute
        {
            var types = executingAssembly.GetTypes().Where(t => t.GetCustomAttribute<TAttribute>() != null);

            return types;
        }

        public static IEnumerable<Type> GetAllTypesWithAttribute<TAttribute>(Type assemblyType)
            where TAttribute : Attribute
        {
            var executingAssembly = assemblyType.Assembly;

            return GetAllTypesWithAttribute<TAttribute>(executingAssembly);
        }
    }
}