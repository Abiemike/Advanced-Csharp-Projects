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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string _connectionString;

        public ApplicantProfileRepository()
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

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            List<ApplicantProfilePoco> result = new List<ApplicantProfilePoco>();

            // Prepare the SQL query to retrieve all records from the Applicant_Profiles table
            string query = @"
            SELECT [Id], [Login], [Current_Salary], [Current_Rate], [Currency],
                   [Country_Code], [State_Province_Code], [Street_Address], 
                   [City_Town], [Zip_Postal_Code]
            FROM [dbo].[Applicant_Profiles];";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map each record to an ApplicantProfilePoco object
                            var poco = new ApplicantProfilePoco
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                 Login = reader.GetGuid(reader.GetOrdinal("Login")), // Assuming Login is a Guid type (foreign key)
                                //Login = reader.GetString(reader.GetOrdinal("Login")),

                                CurrentSalary = reader.GetDecimal(reader.GetOrdinal("Current_Salary")),
                                CurrentRate = reader.GetDecimal(reader.GetOrdinal("Current_Rate")),
                                Currency = reader.GetString(reader.GetOrdinal("Currency")),
                                Country = reader.GetString(reader.GetOrdinal("Country_Code")),
                                Province = reader.GetString(reader.GetOrdinal("State_Province_Code")),
                                Street = reader.GetString(reader.GetOrdinal("Street_Address")),
                                City = reader.GetString(reader.GetOrdinal("City_Town")),
                                PostalCode = reader.GetString(reader.GetOrdinal("Zip_Postal_Code")),
                            };

                            result.Add(poco);
                        }
                    }
                }
            }

            return result;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        // Add: Insert one or more ApplicantProfilePoco items into the database
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var item in items)
                {
                    // Prepare the SQL query to insert a new ApplicantProfilePoco record
                    var query = @"
                    INSERT INTO [dbo].[Applicant_Profiles] 
                    ([Id], [Login], [Current_Salary], [Current_Rate], [Currency],
                     [Country_Code], [State_Province_Code], [Street_Address], 
                     [City_Town], [Zip_Postal_Code]) 
                    VALUES 
                    (@Id, @Login, @CurrentSalary, @CurrentRate, @Currency,
                     @Country, @Province, @Street, @City, @PostalCode);";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                        command.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", item.Currency);
                        command.Parameters.AddWithValue("@Country", item.Country);
                        command.Parameters.AddWithValue("@Province", item.Province);
                        command.Parameters.AddWithValue("@Street", item.Street);
                        command.Parameters.AddWithValue("@City", item.City);
                        command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

                        command.ExecuteNonQuery(); // Execute the insert query
                    }
                }
            }
        }

        // GetSingle: Retrieve a single ApplicantProfilePoco based on a filter expression

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        // Remove: Delete one or more ApplicantProfilePoco items from the database
        public void Remove(params ApplicantProfilePoco[] items)
        {
            using(var connection = new SqlConnection(_connectionString))
        {
                connection.Open();
                foreach (var item in items)
                {
                    // Prepare the SQL query to delete an ApplicantProfilePoco record
                    var query = @"
                    DELETE FROM [dbo].[Applicant_Profiles] 
                    WHERE [Id] = @Id;"
                    ;

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.ExecuteNonQuery(); // Execute the delete query
                    }
                }
            }
        }

        // Update: Update one or more ApplicantProfilePoco items in the database
        public void Update(params ApplicantProfilePoco[] items)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var item in items)
                {
                    // Prepare the SQL query to update an existing ApplicantProfilePoco record
                    var query = @"
                    UPDATE [dbo].[Applicant_Profiles] 
                    SET 
                        [Login] = @Login, 
                        [Current_Salary] = @CurrentSalary,
                        [Current_Rate] = @CurrentRate, 
                        [Currency] = @Currency,
                        [Country_Code] = @Country, 
                        [State_Province_Code] = @Province, 
                        [Street_Address] = @Street, 
                        [City_Town] = @City, 
                        [Zip_Postal_Code] = @PostalCode
                    WHERE [Id] = @Id;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@CurrentSalary", item.CurrentSalary);
                        command.Parameters.AddWithValue("@CurrentRate", item.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", item.Currency);
                        command.Parameters.AddWithValue("@Country", item.Country);
                        command.Parameters.AddWithValue("@Province", item.Province);
                        command.Parameters.AddWithValue("@Street", item.Street);
                        command.Parameters.AddWithValue("@City", item.City);
                        command.Parameters.AddWithValue("@PostalCode", item.PostalCode);

                        command.ExecuteNonQuery(); // Execute the update query
                    }
                }
            }

        }
    }
}