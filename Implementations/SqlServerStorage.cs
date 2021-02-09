using CarbonIntensity.Interfaces;
using CarbonIntensity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarbonIntensity.Implementations
{
    class SqlServerStorage : IDataStorage
    {
        private readonly string connectionString;

        public SqlServerStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Store(IEnumerable<IntensityResponse> records)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var record in records)
                {
                    var command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = @"INSERT INTO CarbonIntensity 
                                                (FromDate, ToDate, ForeCast, Actual, CarbonIndex, DateCreated)
                                                VALUES
                                                (@FromDate, @ToDate, @ForeCast, @Actual, @CarbonIndex, @DateCreated);";

                    command.Parameters.AddWithValue("@FromDate", record.From);
                    command.Parameters.AddWithValue("@ToDate", record.To);
                    command.Parameters.AddWithValue("@ForeCast", record.Intensity.Forecast);
                    command.Parameters.AddWithValue("@Actual", record.Intensity.Actual ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CarbonIndex", record.Intensity.Index);
                    command.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            connection.Close();
        }

        public IntensityResponse GetLatest()
        {
            IntensityResponse response = null;

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var commantText = @"SELECT TOP 1 FromDate, ToDate, ForeCast, Actual, CarbonIndex  FROM CarbonIntensity
                                ORDER BY ToDate DESC;";
            using (var command = new SqlCommand(commantText, connection))
            {
                var dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    response = new IntensityResponse
                    {
                        From = dataReader.GetDateTime(0),
                        To = dataReader.GetDateTime(1),
                        Intensity = new Intensity
                        {
                            Forecast = dataReader.GetInt32(2),
                            Actual = dataReader.GetInt32(3),
                            Index = dataReader.GetString(4)
                        }
                    };
                }
            }

            return response;
        }
    }
}
