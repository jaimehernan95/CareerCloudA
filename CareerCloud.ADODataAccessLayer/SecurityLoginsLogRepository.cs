using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
   public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SecurityLoginsLogRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
           /* _sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                _sqlcon.Open();
                foreach (SecurityLoginsLogPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                       ([Id]
                                       ,[Login]
                                       ,[Source_IP]
                                       ,[Logon_Date]
                                       ,[Is_Succesful])
                                 VALUES
                                       (@Id
                                       ,@Login
                                       ,@Source_IP
                                       ,@Logon_Date
                                       ,@Is_Succesful)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                    
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                _sqlcon.Open();
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                           SET 
                                              [Login] = @Login
                                              ,[Source_IP] = @Source_IP
                                              ,[Logon_Date] = @Logon_Date
                                              ,[Is_Succesful] = @Is_Succesful
                                          WHERE  [Id]= @Id";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                   
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }

  
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Source_IP]
                                  ,[Logon_Date]
                                  ,[Is_Succesful]
                              FROM [dbo].[Security_Logins_Log]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityLoginsLogPoco[] items = new SecurityLoginsLogPoco[3500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginsLogPoco item = new SecurityLoginsLogPoco();
                    item.Id = reader.GetGuid(0);
                    item.Login = reader.GetGuid(1);
                    item.SourceIP = reader.GetString(2);
                    item.LogonDate = reader.GetDateTime(3);
                    item.IsSuccesful = reader.GetBoolean(4);


                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }


        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginsLogPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Security_Logins_Log]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

    
    }
}
