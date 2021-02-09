using CarbonIntensity.Interfaces;
using CarbonIntensity.Models;
using System;
using System.Collections.Generic;

namespace CarbonIntensity.Implementations
{
    public class ApiProvider : IDataProvider
    {
        public ICollection<IntensityResponse> Get()
            => ApiClient.GetAsObjects<List<IntensityResponse>>(Constants.ApiBaseUri).Result;

        public ICollection<IntensityResponse> Get(DateTime start, DateTime end)
            => ApiClient.GetAsObjects<List<IntensityResponse>>($"{Constants.ApiBaseUri}{start.ToISO8601()}/{end.ToISO8601()}").Result;
    }
}