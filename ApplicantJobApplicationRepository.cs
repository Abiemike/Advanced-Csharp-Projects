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
    public class ApplicantJobApplicationRepository :IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connectionString;

    public ApplicantJobApplicationRepository() 
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
        
        // Add: Insert a new ApplicantJobApplicationPoco into the database

        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            foreach (var item in items)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    INSERT INTO [dbo].[Applicant_Job_Applications] ([Id], [Application_Date], [Applicant], [Job])
                    VALUES (@Id, @ApplicationDate, @Applicant, @Job);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Job", item.Job);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            List<ApplicantJobApplicationPoco> result = new List<ApplicantJobApplicationPoco>();

            // SQL query to fetch all records from the Applicant_Job_Applications table
            string query = @"
        SELECT [Id], [Application_Date], [Applicant], [Job]
        FROM [dbo].[Applicant_Job_Applications];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Loop through each row and map to ApplicantJobApplicationPoco
                while (reader.Read())
                {
                    var poco = new ApplicantJobApplicationPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        ApplicationDate = reader.GetDateTime(reader.GetOrdinal("Application_Date")),
                        Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                        Job = reader.GetGuid(reader.GetOrdinal("Job"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where,
            params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        // GetSingle: Retrieve a single ApplicantJobApplicationPoco based on a filter expression
        public ApplicantJobApplicationPoco GetSingle(
            Expression<Func<ApplicantJobApplicationPoco, bool>> where,
            params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
        // Remove: Delete an ApplicantJobApplicationPoco from the database
        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            foreach (var item in items)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM [dbo].[Applicant_Job_Applications] WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
      // Update: Update an existing ApplicantJobApplicationPoco in the database
        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            foreach (var item in items)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    UPDATE [dbo].[Applicant_Job_Applications] 
                    SET [Application_Date] = @ApplicationDate, [Applicant] = @Applicant, [Job] = @Job
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Job", item.Job);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}

