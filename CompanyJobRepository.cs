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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        readonly private string _connectionString;
        public CompanyJobRepository() 
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
        public void Add(params CompanyJobPoco[] items)
        {
          
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int i=0;
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the Company_Jobs table
                    string query = @"
                    INSERT INTO [dbo].[Company_Jobs] ([Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden])
                    VALUES (@Id, @Company, @ProfileCreated, @IsInactive, @IsCompanyHidden)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                    command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Console.WriteLine("after each insertion in Add, i is" + (++i));

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            List<CompanyJobPoco> result = new List<CompanyJobPoco>();

            string query = @"
            SELECT [Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden]
            FROM [dbo].[Company_Jobs];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int i= 0;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new CompanyJobPoco()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Company = reader.GetGuid(reader.GetOrdinal("Company")),
                        ProfileCreated = reader.GetDateTime(reader.GetOrdinal("Profile_Created")),
                        IsInactive = reader.GetBoolean(reader.GetOrdinal("Is_Inactive")),
                        IsCompanyHidden = reader.GetBoolean(reader.GetOrdinal("Is_Company_Hidden"))
                    };
                    Console.WriteLine("after each retrival in GetAll"+(++i));
                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            int i = 0;
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            Console.WriteLine(" In GetSingle after each retrival" + (++i));

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Company_Jobs] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Company_Jobs]
                    SET [Company] = @Company, 
                        [Profile_Created] = @ProfileCreated, 
                        [Is_Inactive] = @IsInactive, 
                        [Is_Company_Hidden] = @IsCompanyHidden
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@ProfileCreated", item.ProfileCreated);
                    command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                    command.Parameters.AddWithValue("@IsCompanyHidden", item.IsCompanyHidden);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}

