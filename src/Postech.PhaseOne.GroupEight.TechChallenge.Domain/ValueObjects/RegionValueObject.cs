using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record RegionValueObject
    {
        public RegionValueObject(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            if (!IsRegionStateAssociatedWithTheCorrectRegion(regionStateName, regionName))
            {
                throw new InvalidRegionException("The state is not associated with the correct region", regionName, regionStateName);
            }
            RegionName = regionName;
            RegionStateName = regionStateName;
        }

        public RegionNameEnumerator RegionName { get; init; }
        public RegionStateNameEnumerator RegionStateName { get; init; }

        private static readonly List<RegionStateNameEnumerator> _midwestStates = new()
        {
            { RegionStateNameEnumerator.Goias },
            { RegionStateNameEnumerator.DistritoFederal },
            { RegionStateNameEnumerator.MatoGrosso },
            { RegionStateNameEnumerator.MatoGrossoDoSul }
        };

        private static readonly List<RegionStateNameEnumerator> _northeastStates = new()
        {
            { RegionStateNameEnumerator.Alagoas },
            { RegionStateNameEnumerator.Bahia },
            { RegionStateNameEnumerator.Ceara },
            { RegionStateNameEnumerator.Maranhao },
            { RegionStateNameEnumerator.Paraiba },
            { RegionStateNameEnumerator.Pernambuco },
            { RegionStateNameEnumerator.Piaui },
            { RegionStateNameEnumerator.RioGrandeDoNorte },
            { RegionStateNameEnumerator.Sergipe }
        };

        private static readonly List<RegionStateNameEnumerator> _northStates = new()
        {
            { RegionStateNameEnumerator.Acre },
            { RegionStateNameEnumerator.Amapa },
            { RegionStateNameEnumerator.Amazonas },
            { RegionStateNameEnumerator.Para },
            { RegionStateNameEnumerator.Rondonia },
            { RegionStateNameEnumerator.Roraima },
            { RegionStateNameEnumerator.Tocantins }
        };

        private static readonly List<RegionStateNameEnumerator> _southeastStates = new()
        {
            { RegionStateNameEnumerator.SaoPaulo },
            { RegionStateNameEnumerator.EspiritoSanto },
            { RegionStateNameEnumerator.RioDeJaneiro },
            { RegionStateNameEnumerator.MinasGerais }
        };

        private static readonly List<RegionStateNameEnumerator> _southStates = new()
        {
            { RegionStateNameEnumerator.RioGrandeDoSul },
            { RegionStateNameEnumerator.Parana },
            { RegionStateNameEnumerator.SantaCatarina },
        };

        private static bool IsRegionStateAssociatedWithTheCorrectRegion(RegionStateNameEnumerator regionStateName, RegionNameEnumerator regionName)
        {
            return regionName switch
            {
                RegionNameEnumerator.Midwest => _midwestStates.Contains(regionStateName),
                RegionNameEnumerator.Northeast => _northeastStates.Contains(regionStateName),
                RegionNameEnumerator.North => _northStates.Contains(regionStateName),
                RegionNameEnumerator.Southeast => _southeastStates.Contains(regionStateName),
                RegionNameEnumerator.South => _southStates.Contains(regionStateName),
                _ => false,
            };
        }
    }
}