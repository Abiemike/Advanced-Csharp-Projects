using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        readonly private string _connectionString;
        public ApplicantSkillRepository()
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

        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                INSERT INTO [dbo].[Applicant_Skills] 
                ([Id],[Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year])
                VALUES
                (@Id, @Applicant, @Skill, @SkillLevel, @StartMonth, @StartYear, @EndMonth, @EndYear);";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id",item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", item.EndMonth);// ?? (object)DBNull.Value
                    command.Parameters.AddWithValue("@EndYear", item.EndYear );//?? (object)DBNull.Value

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

      
        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            List<ApplicantSkillPoco> result = new List<ApplicantSkillPoco>();

            string query = @"
        SELECT [Id], [Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year]
        FROM [dbo].[Applicant_Skills];";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new ApplicantSkillPoco
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Applicant = reader.GetGuid(reader.GetOrdinal("Applicant")),
                        Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                        SkillLevel = reader.IsDBNull(reader.GetOrdinal("Skill_Level")) ? null : reader.GetString(reader.GetOrdinal("Skill_Level")),
                       // StartMonth = reader.GetInt16(reader.GetOrdinal("Start_Month")),
                        StartMonth = reader.GetByte(reader.GetOrdinal("Start_Month")),

                        StartYear = reader.GetInt32(reader.GetOrdinal("Start_Year")),
                        EndMonth = reader.GetByte(reader.GetOrdinal("End_Month")) ,//? (int?)null : reader.GetInt32(reader.GetOrdinal("End_Month")),
                        EndYear = reader.GetInt32(reader.GetOrdinal("End_Year")) //? (int?)null : reader.GetInt32(reader.GetOrdinal("End_Year"))
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }


        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = "DELETE FROM [dbo].[Applicant_Skills] WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    string query = @"
                UPDATE [dbo].[Applicant_Skills]
                SET 
                    [Applicant] = @Applicant, 
                    [Skill] = @Skill, 
                    [Skill_Level] = @SkillLevel, 
                    [Start_Month] = @StartMonth, 
                    [Start_Year] = @StartYear, 
                    [End_Month] = @EndMonth, 
                    [End_Year] = @EndYear
                WHERE [Id] = @Id;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SkillLevel", item.SkillLevel ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StartMonth", item.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", item.StartYear);
                    command.Parameters.AddWithValue("@EndMonth",item.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", item.EndYear);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
