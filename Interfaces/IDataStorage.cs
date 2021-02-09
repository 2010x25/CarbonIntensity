using CarbonIntensity.Models;
using System.Collections.Generic;

namespace CarbonIntensity.Interfaces
{
    public interface IDataStorage
    {
        void Store(IEnumerable<IntensityResponse> responses);

        IntensityResponse GetLatest();
    }
}
