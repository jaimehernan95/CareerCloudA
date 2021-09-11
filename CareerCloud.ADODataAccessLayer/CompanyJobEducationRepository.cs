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
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        private readonly string _connStr;
       /* SqlConnection _sqlcon;*/
        public CompanyJobEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobEducationPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                                       ([Id]
                                       ,[Job]
                                       ,[Major]
                                       ,[Importance])
                                 VALUES
                                       (@Id
                                       ,@Job
                                       ,@Major
                                       ,@Importance)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance);


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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @" SELECT [Id]
                                  ,[Job]
                                  ,[Major]
                                  ,[Importance]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Job_Educations]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobEducationPoco[] items = new CompanyJobEducationPoco[1500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobEducationPoco item = new CompanyJobEducationPoco();
                    item.Id = reader.GetGuid(0);
                    item.Job = reader.GetGuid(1);
                    item.Major = reader.GetString(2);
                    item.Importance = reader.GetInt16(3);
                    item.TimeStamp = (byte[])reader[4];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobEducationPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Job_Educations]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                                       SET 
                                          [Job] = @Job
                                          ,[Major] = @Major
                                          ,[Importance] = @Importance
                                          WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance); ;

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
