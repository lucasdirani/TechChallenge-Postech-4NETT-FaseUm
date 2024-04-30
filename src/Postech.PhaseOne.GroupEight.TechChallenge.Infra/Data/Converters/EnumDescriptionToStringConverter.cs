using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Converters
{
    [ExcludeFromCodeCoverage]
    internal class EnumDescriptionToStringConverter<TEnum>(ConverterMappingHints? mappingHints = null) : ValueConverter<TEnum, string>(
          enumValue => enumValue.GetDescription(),
          stringValue => EnumFromDescriptionHelper<TEnum>.GetValue(stringValue),
          mappingHints)
        where TEnum : Enum
    {
    }
}