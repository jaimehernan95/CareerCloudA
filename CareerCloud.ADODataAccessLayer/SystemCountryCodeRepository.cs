using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SystemCountryCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
           /* _sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params SystemCountryCodePoco[] items)
        {

            using SqlConnection _sqlcon = new SqlConnection(_connStr);
            foreach (SystemCountryCodePoco item in items)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                       ([Code]
                                       ,[Name])
                                 VALUES
                                       (@Code
                                       ,@Name)";


                command.Parameters.AddWithValue("@Code", item.Code);
                command.Parameters.AddWithValue("@Name", item.Name);


                _sqlcon.Open();
                command.ExecuteNonQuery();
                _sqlcon.Close();

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _sqlcon;
                    command.CommandText = @"SELECT [Code]
                                      ,[Name]
                                  FROM [dbo].[System_Country_Codes]";
                    _sqlcon.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    SystemCountryCodePoco[] items = new SystemCountryCodePoco[500];
                    int index = 0;
                    while (reader.Read())
                    {
                        SystemCountryCodePoco item = new SystemCountryCodePoco();
                        item.Code = reader.GetString(0);
                        item.Name = reader.GetString(1);

                        items[index] = item;
                        index++;

                    }
                    _sqlcon.Close();
                    return items.Where(a => a != null).ToList();
                }
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }


        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SystemCountryCodePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[System_Country_Codes]
                                   WHERE  [Code]= @Code";
                    command.Parameters.AddWithValue("Code", item.Code);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                       SET 
                                          [Name] = @Name
                                          WHERE  [Code]= @Code";


                    command.Parameters.AddWithValue("@Code", item.Code);
                    command.Parameters.AddWithValue("@Name", item.Name);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
