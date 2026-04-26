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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        readonly private string _connectionString;
        public SecurityLoginsLogRepository()
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

        // Add method to insert new records into the Security_Logins_Roles table
        public void Add(params SecurityLoginsLogPoco[] items)
        {
          
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the Security_Logins_Log table
                    string query = @"
                INSERT INTO [dbo].[Security_Logins_Log] 
                ([Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful])
                VALUES 
                (@Id, @Login, @SourceIP, @LogonDate, @IsSuccesful);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);  // Ensure the Id is set
                    command.Parameters.AddWithValue("@Login", item.Login);  // Login ID
                    command.Parameters.AddWithValue("@SourceIP", item.SourceIP);  // Source IP address
                    command.Parameters.AddWithValue("@LogonDate", item.LogonDate);  // Logon Date and Time
                    command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);  // Whether the login was successful

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
        }

        // Remove method to delete records from the Security_Logins_Roles table
        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Security_Logins_Log] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // Update method to update existing records in the Security_Logins_Roles table
        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Security_Logins_Log]
                    SET [Login] = @Login, 
                        [Source_IP] = @SourceIP, 
                        [Logon_Date] = @LogonDate, 
                        [Is_Succesful] = @IsSuccesful
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                    command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                    command.Parameters.AddWithValue("@IsSuccesful", item.IsSuccesful);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }

        // GetAll method to retrieve all records from the Security_Logins_Roles table
        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            List<SecurityLoginsLogPoco> result = new List<SecurityLoginsLogPoco>();

            string query = @"
            SELECT [Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful]
            FROM [dbo].[Security_Logins_Log];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new SecurityLoginsLogPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Login = reader.GetGuid(reader.GetOrdinal("Login")),
                        SourceIP = reader.GetString(reader.GetOrdinal("Source_IP")),
                        LogonDate = reader.GetDateTime(reader.GetOrdinal("Logon_Date")),
                        IsSuccesful = reader.GetBoolean(reader.GetOrdinal("Is_Succesful"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        
    }
    
    public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }


    }
}
