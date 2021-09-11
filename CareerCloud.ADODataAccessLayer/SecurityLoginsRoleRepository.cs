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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SecurityLoginsRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginsRolePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                       ([Id]
                                       ,[Login]
                                       ,[Role])
                                 VALUES
                                       (@Id
                                       ,@Login
                                       ,@Role)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Role", item.Role);


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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Role]
                                  ,[Time_Stamp]
                              FROM [dbo].[Security_Logins_Roles]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityLoginsRolePoco[] items = new SecurityLoginsRolePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginsRolePoco item = new SecurityLoginsRolePoco();
                    item.Id = reader.GetGuid(0);
                    item.Login = reader.GetGuid(1);
                    item.Role = reader.GetGuid(2);
                    item.TimeStamp = (byte[])reader[3];



                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginsRolePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Security_Logins_Roles]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }


        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                                           SET 
                                              [Login] = @Login
                                              ,[Role] = @Role
                                          WHERE  [Id]= @Id";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Role", item.Role);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
