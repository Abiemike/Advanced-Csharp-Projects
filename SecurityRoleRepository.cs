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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        readonly private string _connectionString;
        public SecurityRoleRepository() 
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

      public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            List<SecurityRolePoco> result = new List<SecurityRolePoco>();

            string query = @"
            SELECT [Id], [Role], [Is_Inactive]
            FROM [dbo].[Security_Roles];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new SecurityRolePoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Role = reader.GetString(reader.GetOrdinal("Role")),
                        IsInactive = reader.GetBoolean(reader.GetOrdinal("Is_Inactive"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        
    }

    
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the Security_Roles table
                    string query = @"
                    INSERT INTO [dbo].[Security_Roles] 
                    ([Id], [Role], [Is_Inactive])
                    VALUES 
                    (@Id, @Role, @IsInactive);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);  // Ensure the Id is set
                    command.Parameters.AddWithValue("@Role", item.Role);  // Role Name
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);  // Inactive Flag

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Security_Roles]
                    SET [Role] = @Role, 
                        [Is_Inactive] = @IsInactive
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Security_Roles] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
           

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

       

    }
}
