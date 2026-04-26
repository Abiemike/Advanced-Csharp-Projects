using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    /*public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        readonly String _connectionString;
        public ApplicantWorkHistoryRepository()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())  // Set the base path for the app
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Load the appsettings.json file
               .Build();

            // Retrieve the connection string from the configuration
            _connectionString = configuration.GetConnectionString("DataConnection");

            // Check if the connection string is null or empty
            if (string.IsNullOrEmpty(_connectionString))
            {
                Console.WriteLine("Error: Connection string is null or empty. Please check the appsettings.json file.");
                //throw new InvalidOperationException("Connection string is null or empty.");
            }
            else
            {
                // If the connection string is successfully loaded, print it to the console
                // Console.WriteLine("Connection String loaded successfully: " + _connectionString);
            }

        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[Applicant_Work_History] 
                    ([Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], 
                     [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_Year])
                    VALUES
                    (@Applicant, @CompanyName, @CountryCode, @Location, @JobTitle, 
                     @JobDescription, @StartMonth, @StartYear, @EndMonth, @EndYear);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Location", item.Location ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@JobTitle", item.JobTitle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@JobDescription", item.JobDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            List<ApplicantWorkHistoryPoco> result = new List<ApplicantWorkHistoryPoco>();
            string query = @"
            SELECT [Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], [Job_Description],
                   [Start_Month], [Start_Year], [End_Month], [End_Year]
            FROM [dbo].[Applicant_Work_History];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new ApplicantWorkHistoryPoco
                    {
                        Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                        CompanyName = reader.IsDBNull(reader.GetOrdinal("Company_Name")) ? null : reader.GetString(reader.GetOrdinal("Company_Name")),
                        CountryCode = reader.IsDBNull(reader.GetOrdinal("Country_Code")) ? null : reader.GetString(reader.GetOrdinal("Country_Code")),
                        Location = reader.IsDBNull(reader.GetOrdinal("Location")) ? null : reader.GetString(reader.GetOrdinal("Location")),
                        JobTitle = reader.IsDBNull(reader.GetOrdinal("Job_Title")) ? null : reader.GetString(reader.GetOrdinal("Job_Title")),
                        JobDescription = reader.IsDBNull(reader.GetOrdinal("Job_Description")) ? null : reader.GetString(reader.GetOrdinal("Job_Description")),
                        StartMonth =reader.GetInt16(reader.GetOrdinal("Start_Month")),
                        StartYear = reader.GetInt32(reader.GetOrdinal("Start_Year")),
                        EndMonth = reader.GetInt16(reader.GetOrdinal("End_Month")),
                        EndYear = reader.GetInt32(reader.GetOrdinal("End_Year"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    DELETE FROM [dbo].[Applicant_Work_History]
                    WHERE [Applicant] = @Applicant AND [Company_Name] = @CompanyName;"; // Assuming CompanyName is unique for removal

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (var item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Work_History
                                    SET Applicant = @Applicant,
                                        Company_Name = @CompanyName,
                                        Country_Code = @CountryCode,
                                        Location = @Location,
                                        Job_Title = @JobTitle,
                                        Job_Description = @JobDescription,
                                        Start_Month = @StartMonth,
                                        Start_Year = @StartYear,
                                        End_Month = @EndMonth,
                                        End_Year = @EndYear
                                    WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }
    }*/
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connectionString;

        public ApplicantWorkHistoryRepository()
        {
            Console.WriteLine("check connection");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Set the base path for the app
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Load the appsettings.json file
                .Build();

            _connectionString = configuration.GetConnectionString("DataConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                Console.WriteLine("Error: Connection string is null or empty. Please check the appsettings.json file.");

            }
            else
            {
                // If the connection string is successfully loaded, print it to the console
                Console.WriteLine("Connection String loaded successfully: " + _connectionString);
            }
        }

        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (var item in items)
                {
                    command.CommandText = @"INSERT INTO Applicant_Work_History
                                            (Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year)
                                            VALUES
                                            (@Id, @Applicant, @CompanyName, @CountryCode, @Location, @JobTitle, @JobDescription, @StartMonth, @StartYear, @EndMonth, @EndYear)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            var result = new List<ApplicantWorkHistoryPoco>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"SELECT Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year
                                                      FROM Applicant_Work_History", connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new ApplicantWorkHistoryPoco
                    {
                        Id = reader.GetGuid(0),
                        Applicant = reader.GetGuid(1),
                        CompanyName = reader.GetString(2),
                        CountryCode = reader.GetString(3),
                        Location = reader.GetString(4),
                        JobTitle = reader.GetString(5),
                        JobDescription = reader.GetString(6),
                        StartMonth = reader.GetInt16(7),
                        StartYear = reader.GetInt32(8),
                        EndMonth = reader.GetInt16(9),
                        EndYear = reader.GetInt32(10)
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (var item in items)
                {
                    command.CommandText = @"DELETE FROM Applicant_Work_History WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (var item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Work_History
                                            SET Applicant = @Applicant,
                                                Company_Name = @CompanyName,
                                                Country_Code = @CountryCode,
                                                Location = @Location,
                                                Job_Title = @JobTitle,
                                                Job_Description = @JobDescription,
                                                Start_Month = @StartMonth,
                                                Start_Year = @StartYear,
                                                End_Month = @EndMonth,
                                                End_Year = @EndYear
                                            WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@JobTitle", item.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", item.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }

}