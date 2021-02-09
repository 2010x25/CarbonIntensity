using CarbonIntensity.Interfaces;
using System;
using System.Linq;

namespace CarbonIntensity.Implementations
{
    public class Processor : IProcessor
    {
        public IDataProvider dataProvider { get; set; }

        public IDataStorage dataStorage { get; set; }

        public Processor(IDataProvider dataProvider, IDataStorage dataStorage)
        {
            this.dataProvider = dataProvider;
            this.dataStorage = dataStorage;
        }

        public void Process()
        {
            var latestRecord = dataStorage.GetLatest();
            DateTime latestRecordIntervalEnd;

            if (latestRecord == null)
            {
                var records = dataProvider.Get();
                dataStorage.Store(records);
                latestRecordIntervalEnd = records.FirstOrDefault().To;
            }
            else
            {
                latestRecordIntervalEnd = latestRecord.To;
            }

            while (latestRecordIntervalEnd.AddMinutes(Constants.IntervalInMinutes) < DateTime.UtcNow)
            {
                var dateTimeTo = latestRecordIntervalEnd.AddDays(Constants.MaxDateRangeInDays) < DateTime.UtcNow
                    ? latestRecordIntervalEnd.AddDays(Constants.MaxDateRangeInDays)
                    : DateTime.UtcNow;

                var records = dataProvider.Get(latestRecordIntervalEnd.AddMinutes(1), dateTimeTo);
                dataStorage.Store(records);

                latestRecordIntervalEnd = dateTimeTo;
            }
        }
    }
}
