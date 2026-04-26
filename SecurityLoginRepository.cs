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
    public class SecurityLoginRepository: IDataRepository<SecurityLoginPoco>
    {
        readonly string _connectionString;
        public SecurityLoginRepository() 
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
        public void Add(params SecurityLoginPoco[] items)
        {
            if (items == null || items.Length == 0)
            {
                throw new ArgumentException("No items provided for adding.");
            }

            // Build the SQL INSERT query
            var sql = "INSERT INTO [dbo].[Security_Logins] " +
                      "([Id], [Login], [Agreement_Accepted_Date], [Created_Date], [Email_Address], " +
                      "[Force_Change_Password], [Full_Name], [Is_Inactive], [Is_Locked], [Password], " +
                      "[Password_Update_Date], [Phone_Number], [Prefferred_Language]) " +
                      "VALUES (@Id, @Login, @AgreementAccepted, @Created, @EmailAddress, " +
                      "@ForceChangePassword, @FullName, @IsInactive, @IsLocked, @Password, " +
                      "@PasswordUpdate, @PhoneNumber, @PrefferredLanguage);";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Execute the SQL INSERT for each item in the items array
                using (var command = new SqlCommand(sql, connection))
                {
                    // Loop through each item to add
                    foreach (var item in items)
                    {
                        // Add parameters for each property in the SecurityLoginPoco
                        command.Parameters.Clear();  // Clear previous parameters to prevent duplication

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@AgreementAccepted", item.AgreementAccepted);
                        command.Parameters.AddWithValue("@Created", item.Created);
                        command.Parameters.AddWithValue("@EmailAddress", item.EmailAddress);
                        command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                        command.Parameters.AddWithValue("@FullName", item.FullName);
                        command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                        command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                        command.Parameters.AddWithValue("@Password", item.Password);
                        command.Parameters.AddWithValue("@PasswordUpdate", item.PasswordUpdate);
                        command.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                        command.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);

                        try
                        {
                            // Execute the insert command for this SecurityLoginPoco object
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting item with Id {item.Id}: {ex.Message}");
                            throw;
                        }
                    }
                }
            }
        }


        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }
        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var result = new List<SecurityLoginPoco>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                    @"SELECT Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date,
                             Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language
                      FROM Security_Logins", connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var poco = new SecurityLoginPoco
                    {
                        Id = reader.GetGuid(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Created = reader.GetDateTime(3),
                        PasswordUpdate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                        AgreementAccepted = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                        IsLocked = reader.GetBoolean(6),
                        IsInactive = reader.GetBoolean(7),
                        EmailAddress = reader.GetString(8),
                        PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                        FullName = reader.IsDBNull(10) ? null : reader.GetString(10),
                        ForceChangePassword = reader.GetBoolean(11),
                        PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12)
                    };

                    result.Add(poco);
                }

                connection.Close();
            }

            return result;
        }

       public void Remove(params SecurityLoginPoco[] items)
        {
            if (items == null || items.Length == 0)
            {
                throw new ArgumentException("No items provided for removal.");
            }

            // Prepare the SQL query
            var sql = "DELETE FROM [dbo].[Security_Logins] WHERE [Id] IN (" +
                      string.Join(",", items.Select(item => $"'{item.Id}'")) + ");";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Execute the DELETE query
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting items: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            if (items == null || items.Length == 0)
            {
                throw new ArgumentException("No items provided for update.");
            }

            foreach (var item in items)
            {
                // Build the SQL UPDATE query
                var sql = "UPDATE [dbo].[Security_Logins] SET " +
                          "[Login] = @Login, " +
                          "[Agreement_Accepted_Date] = @AgreementAccepted, " +
                          "[Created_Date] = @Created, " +
                          "[Email_Address] = @EmailAddress, " +
                          "[Force_Change_Password] = @ForceChangePassword, " +
                          "[Full_Name] = @FullName, " +
                          "[Is_Inactive] = @IsInactive, " +
                          "[Is_Locked] = @IsLocked, " +
                          "[Password] = @Password, " +
                          "[Password_Update_Date] = @PasswordUpdate, " +
                          "[Phone_Number] = @PhoneNumber, " +
                          "[Prefferred_Language] = @PrefferredLanguage " +
                          "WHERE [Id] = @Id;";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Execute the UPDATE query
                    using (var command = new SqlCommand(sql, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@AgreementAccepted", item.AgreementAccepted);
                        command.Parameters.AddWithValue("@Created", item.Created);
                        command.Parameters.AddWithValue("@EmailAddress", item.EmailAddress);
                        command.Parameters.AddWithValue("@ForceChangePassword", item.ForceChangePassword);
                        command.Parameters.AddWithValue("@FullName", item.FullName);
                        command.Parameters.AddWithValue("@IsInactive", item.IsInactive);
                        command.Parameters.AddWithValue("@IsLocked", item.IsLocked);
                        command.Parameters.AddWithValue("@Password", item.Password);
                        command.Parameters.AddWithValue("@PasswordUpdate", item.PasswordUpdate);
                        command.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                        command.Parameters.AddWithValue("@PrefferredLanguage", item.PrefferredLanguage);
                        command.Parameters.AddWithValue("@Id", item.Id);

                        try
                        {
                            // Execute the command
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating item with Id {item.Id}: {ex.Message}");
                            throw;
                        }
                    }
                }
            }
        }
        IList<SecurityLoginPoco> IDataRepository<SecurityLoginPoco>.GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        void IDataRepository<SecurityLoginPoco>.CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
