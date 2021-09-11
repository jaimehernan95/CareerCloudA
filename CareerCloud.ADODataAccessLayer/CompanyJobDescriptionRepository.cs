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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyJobDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                               ([Id]
                                               ,[Job]
                                               ,[Job_Name]
                                               ,[Job_Descriptions])
                                         VALUES
                                               (@Id
                                               ,@Job
                                               ,@Job_Name
                                               ,@Job_Descriptions)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Job_Name", item.JobName);
                    command.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);


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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @" SELECT
                               [Id]
                              ,[Job]
                              ,[Job_Name]
                              ,[Job_Descriptions]
                              ,[Time_Stamp]
                          FROM [dbo].[Company_Jobs_Descriptions]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobDescriptionPoco[] items = new CompanyJobDescriptionPoco[1500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobDescriptionPoco item = new CompanyJobDescriptionPoco();
                    item.Id = reader.GetGuid(0);
                    item.Job = reader.GetGuid(1);
                    item.JobName = reader.GetString(2);
                    item.JobDescriptions = reader.GetString(3);
                    item.TimeStamp = (byte[])reader[4];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Jobs_Descriptions]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                           SET 
                                              [Job] = @Job
                                              ,[Job_Name] =@Job_Name
                                              ,[Job_Descriptions] = @Job_Descriptions
                                          WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Job_Name", item.JobName);
                    command.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
