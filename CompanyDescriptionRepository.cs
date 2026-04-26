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
     public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
     {readonly private string _connectionString;
        public CompanyDescriptionRepository() 
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
         public void Add(params CompanyDescriptionPoco[] items)
         {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [JOB_PORTAL_DB].[dbo].[Company_Descriptions] 
                    ([Id],[Company], [LanguageID], [Company_Name], [Company_Description])
                    VALUES
                    (@Id,@Company, @LanguageID, @CompanyName, @CompanyDescription);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);  // Add the Id field explicitly
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription ?? (object)DBNull.Value);

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

         public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
         {
            List<CompanyDescriptionPoco> result = new List<CompanyDescriptionPoco>();
            string query = @"
            SELECT [Id], [Company], [LanguageID], [Company_Name], [Company_Description], [Time_Stamp]
            FROM [JOB_PORTAL_DB].[dbo].[Company_Descriptions];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new CompanyDescriptionPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Company = reader.GetGuid(reader.GetOrdinal("Company")),
                        LanguageId = reader.GetString(reader.GetOrdinal("LanguageID")),
                        CompanyName = reader.IsDBNull(reader.GetOrdinal("Company_Name")) ? null : reader.GetString(reader.GetOrdinal("Company_Name")),
                        CompanyDescription = reader.IsDBNull(reader.GetOrdinal("Company_Description")) ? null : reader.GetString(reader.GetOrdinal("Company_Description")),
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

         public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
         {
             throw new NotImplementedException();
         }

         public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
         {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

         public void Remove(params CompanyDescriptionPoco[] items)
         {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    DELETE FROM [JOB_PORTAL_DB].[dbo].[Company_Descriptions]
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

         public void Update(params CompanyDescriptionPoco[] items)
         {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    UPDATE [JOB_PORTAL_DB].[dbo].[Company_Descriptions]
                    SET [Company] = @Company, [LanguageID] = @LanguageID, [Company_Name] = @CompanyName,
                        [Company_Description] = @CompanyDescription
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
  }
  

   
              
