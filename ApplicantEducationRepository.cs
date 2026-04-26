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
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string _connectionString;
        public ApplicantEducationRepository()
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
               Console.WriteLine("Connection String loaded successfully: " + _connectionString);
            }
        }


        public void Add(params ApplicantEducationPoco[] items)
        {
        string query = @"
        INSERT INTO [dbo].[Applicant_Educations]
        ([Id], [Applicant], [Major], [Certificate_Diploma], [Start_Date], [Completion_Date], [Completion_Percent])
        VALUES
        (@Id, @Applicant, @Major, @CertificateDiploma, @StartDate, @CompletionDate, @CompletionPercent)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection created successfully.");

                    foreach (var item in items)
                    {
                        // If the 'Id' is not set (empty), generate a new GUID manually
                        if (item.Id == Guid.Empty)
                        {
                            Console.WriteLine("An ID was Empty and regenerated");
                            item.Id = Guid.NewGuid();  // Generate a new GUID

                        }

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            Console.WriteLine("Id: "+item.Id+" and Applicant" + item.Applicant);
                            // Add parameters for the SQL query
                            cmd.Parameters.AddWithValue("@Id", item.Id);  // Pass the generated GUID
                            cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                            cmd.Parameters.AddWithValue("@Major", item.Major);
                            cmd.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                            cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                            //cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate ?? DBNull.Value);  // Handle nullable field
                            cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                            cmd.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);

                            // Execute the query
                            Console.WriteLine("Executing SQL Command: " + cmd.CommandText);

                            try
                            {
                                int record = cmd.ExecuteNonQuery();
                                Console.WriteLine($"{record} Record/s inserted successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: "+ex.Message);
                            }
                        }
                    }
                    //Console.WriteLine("{record} Record/s inserted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public void Update(ApplicantEducationPoco[] items)
        {
            // SQL UPDATE query to update Applicant Education data
            string query = @"
            UPDATE [dbo].[Applicant_Educations]
            SET 
                [Major] = @Major, 
                [Certificate_Diploma] = @CertificateDiploma, 
                [Start_Date] = @StartDate, 
                [Completion_Date] = @CompletionDate, 
                [Completion_Percent] = @CompletionPercent
            WHERE [Id] = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection created successfully.");

                    foreach (var item in items)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters for the SQL query
                            cmd.Parameters.AddWithValue("@Id", item.Id);  // Update based on the 'Id'
                            cmd.Parameters.AddWithValue("@Major", item.Major);
                            cmd.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                            cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                            cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                            //cmd.Parameters.AddWithValue("@CompletionDate", item.CompletionDate ?? DBNull.Value);  // Handle nullable CompletionDate
                            cmd.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);

                            // Execute the query
                            int affectedRecord = cmd.ExecuteNonQuery();
                            Console.WriteLine($" {affectedRecord}Record with Id {item.Id} updated successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>>
           where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            List<ApplicantEducationPoco> result = new List<ApplicantEducationPoco>();

            // SQL query to retrieve all records from the Applicant_Educations table
            string query = @"
        SELECT [Id], [Applicant], [Major], [Certificate_Diploma], [Start_Date], 
               [Completion_Date], [Completion_Percent]
        FROM [dbo].[Applicant_Educations];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Loop through each row returned from the database
                while (reader.Read())
                {
                    var poco = new ApplicantEducationPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                        Major = reader.IsDBNull(reader.GetOrdinal("Major")) ? null : reader.GetString(reader.GetOrdinal("Major")),
                        CertificateDiploma = reader.IsDBNull(reader.GetOrdinal("Certificate_Diploma")) ? null : reader.GetString(reader.GetOrdinal("Certificate_Diploma")),
                        StartDate = reader.IsDBNull(reader.GetOrdinal("Start_Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Start_Date")),
                        CompletionDate = reader.IsDBNull(reader.GetOrdinal("Completion_Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Completion_Date")),
                        CompletionPercent = reader.IsDBNull(reader.GetOrdinal("Completion_Percent")) ? (byte)0 : reader.GetByte(reader.GetOrdinal("Completion_Percent"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }
        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>>
            where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            string query = @"
            DELETE FROM [dbo].[Applicant_Educations]
            WHERE [Id] = @Id;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (var item in items)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add the parameter for the Id
                            command.Parameters.AddWithValue("@Id", item.Id);

                            // Execute the delete command
                            int rowsAffected = command.ExecuteNonQuery();

                            // You could log the number of rows affected if needed
                            Console.WriteLine($"Deleted {rowsAffected} record(s) with Id = {item.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle exceptions as appropriate
                }
            }
        }

        
    }
} 



 










