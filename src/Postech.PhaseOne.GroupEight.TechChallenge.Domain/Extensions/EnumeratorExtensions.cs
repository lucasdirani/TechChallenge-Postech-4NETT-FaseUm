using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class EnumeratorExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo is null) return string.Empty;
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}