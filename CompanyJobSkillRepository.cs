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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        readonly private string _connectionString;
        public CompanyJobSkillRepository() 
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
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to insert a new record into the Company_Job_Skills table
                    string query = @"
                    INSERT INTO [dbo].[Company_Job_Skills] ([Id], [Job], [Skill], [Skill_Level], [Importance])
                    VALUES (@Id, @Job, @Skill, @SkillLevel, @Importance);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to delete a record based on Id
                    string query = "DELETE FROM [dbo].[Company_Job_Skills] WHERE [Id] = @Id;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    // SQL query to update a record based on Id
                    string query = @"
                    UPDATE [dbo].[Company_Job_Skills]
                    SET [Job] = @Job, 
                        [Skill] = @Skill, 
                        [Skill_Level] = @SkillLevel, 
                        [Importance] = @Importance
                    WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            List<CompanyJobSkillPoco> result = new List<CompanyJobSkillPoco>();

            string query = @"
            SELECT [Id], [Job], [Skill], [Skill_Level], [Importance]
            FROM [dbo].[Company_Job_Skills];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var poco = new CompanyJobSkillPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Job = reader.GetGuid(reader.GetOrdinal("Job")),
                        Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                        SkillLevel = reader.IsDBNull(reader.GetOrdinal("Skill_Level")) ? null : reader.GetString(reader.GetOrdinal("Skill_Level")),
                        Importance = reader.GetInt32(reader.GetOrdinal("Importance"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }
    



    public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }
       
        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
