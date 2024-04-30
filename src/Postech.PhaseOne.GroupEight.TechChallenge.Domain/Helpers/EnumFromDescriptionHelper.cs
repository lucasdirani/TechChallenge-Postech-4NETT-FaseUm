using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class EnumFromDescriptionHelper<T> 
        where T : Enum
    {
        public static T GetValue(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException($"{type.Name} is not an Enum");
            }
            foreach (FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute attribute 
                    && attribute.Description == description)
                {
                    object? value = field.GetValue(null);
                    return value is null ? throw new InvalidOperationException($"Cannot convert description '{description}'.") : (T) value;
                }
            }
            throw new ArgumentException($"No enum value with description '{description}' found.");
        }
    }
}