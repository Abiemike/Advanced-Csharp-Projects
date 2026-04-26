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
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        readonly private string _connectionString;
        public CompanyJobEducationRepository()
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
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[Company_Job_Educations] 
                    ([Id], [Job], [Major], [Importance]) 
                    VALUES 
                    (@Id, @Job, @Major, @Importance);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
              List<CompanyJobEducationPoco> result = new List<CompanyJobEducationPoco>();

              string query = @"
              SELECT [Id], [Job], [Major], [Importance]
              FROM [dbo].[Company_Job_Educations];";

              using (SqlConnection connection = new SqlConnection(_connectionString))
              {
                  SqlCommand command = new SqlCommand(query, connection);
                  connection.Open();

                  SqlDataReader reader = command.ExecuteReader();
                  while (reader.Read())
                  {
                      var poco = new CompanyJobEducationPoco
                      {
                          Id = reader.GetGuid(reader.GetOrdinal("Id")),
                          Job = reader.GetGuid(reader.GetOrdinal("Job")),
                          Major = reader.IsDBNull(reader.GetOrdinal("Major")) ? null : reader.GetString(reader.GetOrdinal("Major")),
                          Importance = reader.GetInt16(reader.GetOrdinal("Importance"))
                      };

                      result.Add(poco);
                  }

                  connection.Close();
              }

              return result;
          
        }
    

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Company_Job_Educations] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Company_Job_Educations]
                    SET [Job] = @Job, [Major] = @Major, [Importance] = @Importance
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }

}

