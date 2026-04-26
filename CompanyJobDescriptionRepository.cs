using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    { readonly private string _connectionString;
        public CompanyJobDescriptionRepository()
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
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand(
                        @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                ([Id], [Job], [Job_Name], [Job_Descriptions])
                VALUES
                (@Id, @Job, @JobName, @JobDescriptions)",
                        connection);

                    // Parameters for the query
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@JobName", item.JobName);// ?? (object)DBNull.Value);  // Handle null for JobName
                    command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);//?? (object)DBNull.Value);  // Handle null for JobDescriptions

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            List<CompanyJobDescriptionPoco> result = new List<CompanyJobDescriptionPoco>();

            string query = @"
        SELECT [Id], [Job], [Job_Name], [Job_Descriptions]
        FROM [dbo].[Company_Jobs_Descriptions];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new CompanyJobDescriptionPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Job = reader.GetGuid(reader.GetOrdinal("Job")),
                        JobName = reader.IsDBNull(reader.GetOrdinal("Job_Name")) ? null : reader.GetString(reader.GetOrdinal("Job_Name")),
                        JobDescriptions = reader.IsDBNull(reader.GetOrdinal("Job_Descriptions")) ? null : reader.GetString(reader.GetOrdinal("Job_Descriptions")),
                    };

                    result.Add(poco);
                }

                connection.Close();
            }
            return result;

        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
            {

            throw new NotImplementedException();
            }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
            {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

            public void Remove(params CompanyJobDescriptionPoco[] items)
            {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand(
                        @"DELETE FROM [dbo].[Company_Jobs_Descriptions] WHERE [Id] = @Id", connection);

                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

            public void Update(params CompanyJobDescriptionPoco[] items)
            {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand(
                        @"UPDATE [dbo].[Company_Jobs_Descriptions]
                SET [Job] = @Job,
                    [Job_Name] = @JobName,
                    [Job_Descriptions] = @JobDescriptions
                   
                WHERE [Id] = @Id", connection);

                    // Parameters for the query
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@JobName", item.JobName ?? (object)DBNull.Value);  // Handle null for JobName
                    command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions ?? (object)DBNull.Value);  // Handle null for JobDescriptions

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
    }