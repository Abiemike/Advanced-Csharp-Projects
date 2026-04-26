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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        readonly private string _connectionString;
        public SystemLanguageCodeRepository()
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
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    INSERT INTO [dbo].[System_Language_Codes] 
                    ([LanguageID], [Name], [Native_Name])
                    VALUES 
                    (@LanguageID, @Name, @NativeName);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);  // LanguageID (unique identifier)
                    command.Parameters.AddWithValue("@Name", item.Name);  // Name of the language (max 50 characters)
                    command.Parameters.AddWithValue("@NativeName", item.NativeName);  // Native name of the language (max 50 characters)

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                    UPDATE [dbo].[System_Language_Codes]
                    SET [Name] = @Name, [Native_Name] = @NativeName
                    WHERE [LanguageID] = @LanguageID;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@NativeName", item.NativeName);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = "DELETE FROM [dbo].[System_Language_Codes] WHERE [LanguageID] = @LanguageID;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            List<SystemLanguageCodePoco> result = new List<SystemLanguageCodePoco>();

            string query = @"
            SELECT [LanguageID], [Name], [Native_Name]
            FROM [dbo].[System_Language_Codes];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new SystemLanguageCodePoco
                    {
                        LanguageID = reader.GetString(reader.GetOrdinal("LanguageID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        NativeName = reader.GetString(reader.GetOrdinal("Native_Name"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }


    public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
                    

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

    }
}
