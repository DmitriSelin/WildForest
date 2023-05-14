using System;

namespace WildForest.Frontend.Contracts.Maps
{
    internal class CityDto
    {
        public Guid CityId { get; }
        
        public string CityName { get; }

        public CityDto(Guid cityId, string cityName)
        {
            CityId = cityId;
            CityName = cityName;
        }

        public override string ToString()
        {
            return CityName;
        }
    }
}