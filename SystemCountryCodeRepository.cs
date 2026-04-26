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

    
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        readonly private string _connectionString;
        public SystemCountryCodeRepository() 
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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            List<SystemCountryCodePoco> result = new List<SystemCountryCodePoco>();

            string query = @"
            SELECT [Code], [Name]
            FROM [dbo].[System_Country_Codes];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new SystemCountryCodePoco
                    {
                        Code = reader.GetString(reader.GetOrdinal("Code")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }


        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[System_Country_Codes] 
                    ([Code], [Name])
                    VALUES 
                    (@Code, @Name);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Code", item.Code);  // Code for country (10 characters)
                    command.Parameters.AddWithValue("@Name", item.Name);  // Name of the country (max 50 characters)

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    UPDATE [dbo].[System_Country_Codes]
                    SET [Name] = @Name
                    WHERE [Code] = @Code;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Code", item.Code);
                    command.Parameters.AddWithValue("@Name", item.Name);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = "DELETE FROM [dbo].[System_Country_Codes] WHERE [Code] = @Code;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Code", item.Code);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }


        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
