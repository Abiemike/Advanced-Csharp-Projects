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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    { readonly string _connectionString;
        public ApplicantResumeRepository() 
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
        public void Add(params ApplicantResumePoco[] items)
        {
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    foreach (var item in items)
                    {
                        SqlCommand command = new SqlCommand(@"INSERT INTO [dbo].[Applicant_Resumes]
                                                                ([Id],[Applicant], [Resume], [Last_Updated])
                                                                VALUES
                                                                (@Id,@Applicant, @Resume, @LastUpdated)", connection);
                        // Add parameters for each column in the table
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Resume", item.Resume ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);

                        // Open the connection, execute the command, and close the connection
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
        
            List<ApplicantResumePoco> result = new List<ApplicantResumePoco>();

         string query = @"
        SELECT [Id], [Applicant], [Resume], [Last_Updated]
        FROM [dbo].[Applicant_Resumes];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new ApplicantResumePoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                        Resume = reader.IsDBNull(reader.GetOrdinal("Resume")) ? null : reader.GetString(reader.GetOrdinal("Resume")),
                    
                     LastUpdated= reader.IsDBNull(reader.GetOrdinal("Last_Updated")) ? (DateTime?)null: reader.GetDateTime(reader.GetOrdinal("Last_Updated"))
                    };

                    /*Safely check for LastUpdated column
                    if (!reader.IsDBNull(reader.GetOrdinal("Last_Updated")))
                    {
                        poco.LastUpdated = reader.GetDateTime(reader.GetOrdinal("Last_Updated"));
                    }
                    else
                    {
                        poco.LastUpdated = null; // Assign null if LastUpdated is NULL
                    }*/

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete the record by Id
                    string query = @"
                DELETE FROM [dbo].[Applicant_Resumes]
                WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            // Open the connection once outside the loop for efficiency
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();  // Open the connection once before the loop

                foreach (var item in items)
                {
                    // SQL query to update the record by Id
                    string query = @"
                UPDATE [dbo].[Applicant_Resumes]
                SET [Resume] = @Resume, [Last_Updated] = @LastUpdated
                WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameters for the update fields
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Resume", item.Resume ?? (object)DBNull.Value); // Handle null Resume
                    command.Parameters.AddWithValue("@LastUpdated", item.LastUpdated);

                    // Execute the command for each item
                    command.ExecuteNonQuery();
                }

                connection.Close();  // Close the connection after all updates are done
            }
        }
    }
}
