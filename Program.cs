using CarbonIntensity.Implementations;
using System;
using System.Threading.Tasks;

namespace CarbonIntensity
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dataProvider = new ApiProvider();
            var dataStorage = new SqlServerStorage(Constants.DbConnectionString);
            var processor = new Processor(dataProvider, dataStorage);

            while (true)
            {
                Console.WriteLine("----------- Fetching API service -------");
                try
                {
                    processor.Process();
                }
                catch (Exception)
                {
                    break;  // Assume momentary outrage of API or DB, try again in next interval as specified in Constants
                }
                
                Console.WriteLine("---------Going into Sleep model-----------");

                await Task.Delay(Constants.IntervalInMinutes * 60 * 1000);
            }
        }
    }
}
