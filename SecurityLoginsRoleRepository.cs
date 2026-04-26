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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        readonly string _connectionString;
        public SecurityLoginsRoleRepository()
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

        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[Security_Logins_Roles] 
                    ([Id], [Login], [Role])
                    VALUES 
                    (@Id, @Login, @Role);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);  // Guid for Id
                    command.Parameters.AddWithValue("@Login", item.Login);  // Login Id
                    command.Parameters.AddWithValue("@Role", item.Role);  // Role Id

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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            List<SecurityLoginsRolePoco> result = new List<SecurityLoginsRolePoco>();

            string query = @"
            SELECT [Id], [Login], [Role]
            FROM [dbo].[Security_Logins_Roles];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new SecurityLoginsRolePoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Login = reader.GetGuid(reader.GetOrdinal("Login")),  // Assuming Login is a GUID
                        Role = reader.GetGuid(reader.GetOrdinal("Role"))  // Assuming Role is a GUID
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        
    }


            public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Security_Logins_Roles] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    UPDATE [dbo].[Security_Logins_Roles]
                    SET [Login] = @Login, 
                        [Role] = @Role
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Role", item.Role);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}