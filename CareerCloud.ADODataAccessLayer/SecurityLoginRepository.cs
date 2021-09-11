using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                       ([Id]
                                       ,[Login]
                                       ,[Password]
                                       ,[Created_Date]
                                       ,[Password_Update_Date]
                                       ,[Agreement_Accepted_Date]
                                       ,[Is_Locked]
                                       ,[Is_Inactive]
                                       ,[Email_Address]
                                       ,[Phone_Number]
                                       ,[Full_Name]
                                       ,[Force_Change_Password]
                                       ,[Prefferred_Language])
                                 VALUES
                                       (@Id
                                       ,@Login
                                       ,@Password
                                       ,@Created_Date
                                       ,@Password_Update_Date
                                       ,@Agreement_Accepted_Date
                                       ,@Is_Locked
                                       ,@Is_Inactive
                                       ,@Email_Address
                                       ,@Phone_Number
                                       ,@Full_Name
                                       ,@Force_Change_Password
                                       ,@Prefferred_Language)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@Created_Date", item.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", item.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);



                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Password]
                                  ,[Created_Date]
                                  ,[Password_Update_Date]
                                  ,[Agreement_Accepted_Date]
                                  ,[Is_Locked]
                                  ,[Is_Inactive]
                                  ,[Email_Address]
                                  ,[Phone_Number]
                                  ,[Full_Name]
                                  ,[Force_Change_Password]
                                  ,[Prefferred_Language]
                                  ,[Time_Stamp]
                              FROM [dbo].[Security_Logins]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityLoginPoco[] items = new SecurityLoginPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginPoco item = new SecurityLoginPoco();
                    item.Id = reader.GetGuid(0);
                    item.Login = reader.GetString(1);
                    item.Password = reader.GetString(2);
                    item.Created = reader.GetDateTime(3);
                    item.PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                    item.AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                    item.IsLocked = reader.GetBoolean(6);
                    item.IsInactive = reader.GetBoolean(7);
                    item.EmailAddress = reader.GetString(8);
                    item.PhoneNumber = reader.IsDBNull(9) ? (string)null : reader.GetString(9);
                    item.FullName = reader.GetString(10);
                    item.ForceChangePassword = reader.GetBoolean(11);
                    item.PrefferredLanguage = reader.IsDBNull(12) ? (string)null : reader.GetString(12);
                    item.TimeStamp = (byte[])reader[13];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Security_Logins]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }


        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Security_Logins]
                                       SET
                                          [Login] = @Login
                                          ,[Password] = @Password
                                          ,[Created_Date] = @Created_Date
                                          ,[Password_Update_Date] = @Password_Update_Date
                                          ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                                          ,[Is_Locked] =@Is_Locked
                                          ,[Is_Inactive] = @Is_Inactive
                                          ,[Email_Address] = @Email_Address
                                          ,[Phone_Number] = @Phone_Number
                                          ,[Full_Name] = @Full_Name
                                          ,[Force_Change_Password] = @Force_Change_Password
                                          ,[Prefferred_Language] = @Prefferred_Language
                                          WHERE  [Id]= @Id";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@Created_Date", item.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", item.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
