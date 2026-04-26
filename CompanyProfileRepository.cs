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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        readonly private string _connectionString;

        public CompanyProfileRepository() 
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
        // Add method to insert new records into the Company_Profiles table
        // Add method to insert new records into the Company_Profiles table
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the Company_Profiles table
                    string query = @"
                    INSERT INTO [dbo].[Company_Profiles] 
                    ([Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo])
                    VALUES 
                    (@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);  // Ensure the Id is set
                    command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                    command.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                    command.Parameters.AddWithValue("@ContactName", item.ContactName);
                    command.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Update method to update existing records in the Company_Profiles table
        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Company_Profiles]
                    SET 
                        [Registration_Date] = @RegistrationDate, 
                        [Company_Website] = @CompanyWebsite, 
                        [Contact_Phone] = @ContactPhone,
                        [Contact_Name] = @ContactName, 
                        [Company_Logo] = @CompanyLogo
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@RegistrationDate", item.RegistrationDate);
                    command.Parameters.AddWithValue("@CompanyWebsite", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@ContactPhone", item.ContactPhone);
                    command.Parameters.AddWithValue("@ContactName", item.ContactName);
                    command.Parameters.AddWithValue("@CompanyLogo", item.CompanyLogo);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Remove method to delete records from the Company_Profiles table
        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Company_Profiles] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // GetAll method to retrieve all records from the Company_Profiles table
        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            List<CompanyProfilePoco> result = new List<CompanyProfilePoco>();

            string query = @"
            SELECT [Id], [Registration_Date], [Company_Website], [Contact_Phone], 
                   [Contact_Name], [Company_Logo]
            FROM [dbo].[Company_Profiles];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new CompanyProfilePoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        RegistrationDate = reader.GetDateTime(reader.GetOrdinal("Registration_Date")),
                        CompanyWebsite = reader.IsDBNull(reader.GetOrdinal("Company_Website")) ? null : reader.GetString(reader.GetOrdinal("Company_Website")),
                        ContactPhone = reader.IsDBNull(reader.GetOrdinal("Contact_Phone")) ? null : reader.GetString(reader.GetOrdinal("Contact_Phone")),
                        ContactName = reader.IsDBNull(reader.GetOrdinal("Contact_Name")) ? null : reader.GetString(reader.GetOrdinal("Contact_Name")),
                        CompanyLogo = reader.IsDBNull(reader.GetOrdinal("Company_Logo")) ? null : (byte[])reader["Company_Logo"]
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
   
    }
   
}
