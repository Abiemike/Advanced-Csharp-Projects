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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        readonly private string _connectionString;

        public CompanyLocationRepository()
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
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[Company_Locations] 
                    ([Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code]) 
                    VALUES 
                    (@Id, @Company, @CountryCode, @Province, @Street, @City, @PostalCode);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Province", item.Province);
                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    UPDATE [dbo].[Company_Locations]
                    SET 
                        [Company] = @Company,
                        [Country_Code] = @CountryCode,
                        [State_Province_Code] = @Province,
                        [Street_Address] = @Street,
                        [City_Town] = @City,
                        [Zip_Postal_Code] = @PostalCode
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@Province", item.Province);
                    command.Parameters.AddWithValue("@Street", item.Street);
                    command.Parameters.AddWithValue("@City", item.City);
                    command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = "DELETE FROM [dbo].[Company_Locations] WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            List<CompanyLocationPoco> result = new List<CompanyLocationPoco>();

            string query = @"
        SELECT [Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code] 
        FROM [dbo].[Company_Locations];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new CompanyLocationPoco
                    {
                        // Ensure that we read Guid and handle DBNull where necessary
                        Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("Id")),
                        Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("Company")),

                        // Safely retrieve strings and handle DBNull
                        CountryCode = reader.IsDBNull(reader.GetOrdinal("Country_Code")) ? null : reader.GetString(reader.GetOrdinal("Country_Code")),
                        Province = reader.IsDBNull(reader.GetOrdinal("State_Province_Code")) ? null : reader.GetString(reader.GetOrdinal("State_Province_Code")),
                        Street = reader.IsDBNull(reader.GetOrdinal("Street_Address")) ? null : reader.GetString(reader.GetOrdinal("Street_Address")),
                        City = reader.IsDBNull(reader.GetOrdinal("City_Town")) ? null : reader.GetString(reader.GetOrdinal("City_Town")),
                        PostalCode = reader.IsDBNull(reader.GetOrdinal("Zip_Postal_Code")) ? null : reader.GetString(reader.GetOrdinal("Zip_Postal_Code"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;

        }
        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
    IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();  
        }
         
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

     

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

      
        
    }
}
