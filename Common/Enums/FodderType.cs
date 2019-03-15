using System.ComponentModel;

namespace Common.Enums
{
    public enum FodderType
    {
        [Description("Sucha")]
        Dry = 1,

        [Description("Soczysta")]
        Juicy = 2,

        [Description("Treściwa")]
        Pithy = 3,

        [Description("Sól")]
        Salt = 4
    }
}
