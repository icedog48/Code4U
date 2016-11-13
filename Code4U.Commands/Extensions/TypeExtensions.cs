using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Commands
{
    public static class TypeExtensions
    {
        public static string ToGenericTypeString(this Type type)
        {
            if (!type.IsGenericType) return type.GetSystemType();

            var genericTypeName = type.GetGenericTypeDefinition().Name;

            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

            var genericArgs = string.Join(",", type.GetGenericArguments().Select(ta => ToGenericTypeString(ta)).ToArray());

            return genericTypeName + "<" + genericArgs + ">";
        }

        public static string GetSystemType(this Type type)
        {
            if (type.Name == "Int32") return "int";

            if (type.Name == "String") return "string";

            if (type.Name == "Boolean") return "bool";

            return type.Name;
        }
    }
}
