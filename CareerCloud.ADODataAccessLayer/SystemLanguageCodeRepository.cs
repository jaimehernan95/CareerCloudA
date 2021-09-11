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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public SystemLanguageCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
           /* _sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SystemLanguageCodePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                           ([LanguageID]
                                           ,[Name]
                                           ,[Native_Name])
                                     VALUES
                                           (@LanguageID
                                           ,@Name
                                           ,@Native_Name)";


                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Native_Name", item.NativeName);


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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [LanguageID]
                                      ,[Name]
                                      ,[Native_Name]
                                  FROM [dbo].[System_Language_Codes]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                SystemLanguageCodePoco[] items = new SystemLanguageCodePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco item = new SystemLanguageCodePoco();
                    item.LanguageID = reader.GetString(0);
                    item.Name = reader.GetString(1);
                    item.NativeName = reader.GetString(2);

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }


        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (SystemLanguageCodePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[System_Language_Codes]
                                   WHERE  [LanguageID]= @LanguageID";
                    command.Parameters.AddWithValue("LanguageID", item.LanguageID);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                       SET 
                                          [Name] = @Name
                                          ,[Native_Name] = @Native_Name
                                          WHERE  [LanguageID]= @LanguageID";


                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Native_Name", item.NativeName);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
