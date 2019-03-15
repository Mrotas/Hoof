using System;
using System.Linq;

namespace Common.Extensions
{
    public class TypeName
    {
        public static string GetFodderTypeName(int fodderType)
        {
            switch (fodderType)
            {
                case 1: return ToUpperCamelCase(Text.Dry);
                case 2: return ToUpperCamelCase(Text.Juicy);
                case 3: return ToUpperCamelCase(Text.Pithy);
                case 4: return ToUpperCamelCase(Text.Salt);
                default: throw new NotImplementedException();
            }
        }

        private static string ToUpperCamelCase(string text)
        {
            return Char.ToUpper(text.ElementAt(0)) + text.Substring(1);
        }
    }
}
