using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record AreaCodeValueObject
    {
        public string Value { get; }
        public RegionValueObject Region { get; }

        private AreaCodeValueObject(string value, RegionValueObject region) 
            : this(value)
        {
            Region = region;
        }

        private AreaCodeValueObject(string value)
        {
            Value = value;
        }

        public static AreaCodeValueObject Create(string areaCodeValue)
        {
            return areaCodeValue switch
            {
                "11" or "12" or "13" or "14" or "15" or "16" or "17" or "18" or "19" => new(areaCodeValue, new(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.SaoPaulo)),
                "21" or "22" or "24" => new(areaCodeValue, new(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.RioDeJaneiro)),
                "27" or "28" => new(areaCodeValue, new(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.EspiritoSanto)),
                "31" or "32" or "33" or "34" or "35" or "37" or "38" => new(areaCodeValue, new(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.MinasGerais)),
                "41" or "42" or "43" or "44" or "45" or "46" => new(areaCodeValue, new(RegionNameEnumerator.South, RegionStateNameEnumerator.Parana)),
                "47" or "48" or "49" => new(areaCodeValue, new(RegionNameEnumerator.South, RegionStateNameEnumerator.SantaCatarina)),
                "51" or "53" or "54" or "55" => new(areaCodeValue, new(RegionNameEnumerator.South, RegionStateNameEnumerator.RioGrandeDoSul)),
                "61" => new(areaCodeValue, new(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.DistritoFederal)),
                "62" or "64" => new(areaCodeValue, new(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.Goias)),
                "63" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Tocantins)),
                "65" or "66" => new(areaCodeValue, new(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.MatoGrosso)),
                "67" => new(areaCodeValue, new(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.MatoGrossoDoSul)),
                "68" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Acre)),
                "69" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Rondonia)),
                "71" or "73" or "74" or "75" or "77" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Bahia)),
                "79" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Sergipe)),
                "81" or "87" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Pernambuco)),
                "82" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Alagoas)),
                "83" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Paraiba)),
                "84" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.RioGrandeDoNorte)),
                "85" or "88" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Ceara)),
                "86" or "89" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Piaui)),
                "91" or "93" or "94" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Para)),
                "92" or "97" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Amazonas)),
                "95" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Roraima)),
                "96" => new(areaCodeValue, new(RegionNameEnumerator.North, RegionStateNameEnumerator.Amapa)),
                "98" or "99" => new(areaCodeValue, new(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Maranhao)),
                _ => throw new AreaCodeValueNotSupportedException("The area code is not in the supported code list", areaCodeValue),
            };
        }
    }
}