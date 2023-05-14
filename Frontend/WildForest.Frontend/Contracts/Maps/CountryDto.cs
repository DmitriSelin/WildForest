using System;

namespace WildForest.Frontend.Contracts.Maps
{
    internal sealed class CountryDto
    {
        public Guid CountryId { get; }

        public string CountryName { get; }

        public CountryDto(Guid countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
        }

        public override string ToString()
        {
            return CountryName;
        }
    }
}