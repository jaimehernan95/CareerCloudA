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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyDescriptionPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                       ([Id]
                                       ,[Company]
                                       ,[LanguageID]
                                       ,[Company_Name]
                                       ,[Company_Description])
                                 VALUES
                                       (@Id
                                       ,@Company
                                       ,@LanguageID
                                       ,@Company_Name
                                       ,@Company_Description)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"
                             SELECT [Id]
                              ,[Company]
                              ,[LanguageID]
                              ,[Company_Name]
                              ,[Company_Description]
                              ,[Time_Stamp]
                          FROM [dbo].[Company_Descriptions]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyDescriptionPoco[] items = new CompanyDescriptionPoco[1500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco item = new CompanyDescriptionPoco();
                    item.Id = reader.GetGuid(0);
                    item.Company = reader.GetGuid(1);
                    item.LanguageId = reader.GetString(2);
                    item.CompanyName = reader.GetString(3);
                    item.CompanyDescription = reader.GetString(4);
                    item.TimeStamp = (byte[])reader[5];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }


        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyDescriptionPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Descriptions]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }


        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                       SET
                                          [Company] = @Company
                                          ,[LanguageID] = @LanguageID
                                          ,[Company_Name] = @Company_Name
                                          ,[Company_Description] =@Company_Description
                                          WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
