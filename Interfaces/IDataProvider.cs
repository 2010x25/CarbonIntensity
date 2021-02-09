using CarbonIntensity.Models;
using System;
using System.Collections.Generic;

namespace CarbonIntensity.Interfaces
{
    public interface IDataProvider
    {
        ICollection<IntensityResponse> Get();

        ICollection<IntensityResponse> Get(DateTime start, DateTime end);
    }
}
