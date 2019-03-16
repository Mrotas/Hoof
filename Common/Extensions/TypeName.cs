using System;
using System.Linq;

namespace Common.Extensions
{
    public class TypeName
    {
        public static string GetEmploymentTypeName(int employmentType)
        {
            switch (employmentType)
            {
                case 1: return ToUpperCamelCase(Text.ContractEmployees);
                case 2: return ToUpperCamelCase(Text.NonContractEmployees);
                default: throw new NotImplementedException();
            }
        }

        public static string GetHuntEquipmentTypeName(int huntEquipmentType)
        {
            switch (huntEquipmentType)
            {
                case 1: return ToUpperCamelCase(Text.Pastures);
                case 2: return ToUpperCamelCase(Text.DeerLickers);
                case 3: return ToUpperCamelCase(Text.Pulpits);
                case 4: return ToUpperCamelCase(Text.Aviaries);
                case 5: return ToUpperCamelCase(Text.Farms);
                case 6: return ToUpperCamelCase(Text.Other);
                default: throw new NotImplementedException();
            }
        }

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
