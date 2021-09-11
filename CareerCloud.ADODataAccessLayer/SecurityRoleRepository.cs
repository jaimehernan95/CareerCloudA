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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SecurityRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }

        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityRolePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                       ([Id]
                                       ,[Role]
                                       ,[Is_Inactive])
                                 VALUES
                                       (@Id
                                       ,@Role
                                       ,@Is_Inactive)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);


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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command  = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                  ,[Role]
                                  ,[Is_Inactive]
                              FROM [dbo].[Security_Roles]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                SecurityRolePoco[] items = new SecurityRolePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityRolePoco item = new SecurityRolePoco();
                    item.Id = reader.GetGuid(0);
                    item.Role = reader.GetString(1);
                    item.IsInactive = reader.GetBoolean(2);


                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }


        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }
        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SecurityRolePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Security_Roles]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Security_Roles]
                                       SET 
                                          [Role] = @Role
                                          ,[Is_Inactive] = @Is_Inactive
                                          WHERE  [Id]= @Id";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
