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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyJobSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobSkillPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                       ([Id]
                                       ,[Job]
                                       ,[Skill]
                                       ,[Skill_Level]
                                       ,[Importance])
                                 VALUES
                                       (@Id
                                       ,@Job
                                       ,@Skill
                                       ,@Skill_Level
                                       ,@Importance)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Skill]
                                      ,[Skill_Level]
                                      ,[Importance]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Skills]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobSkillPoco[] items = new CompanyJobSkillPoco[6500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobSkillPoco item = new CompanyJobSkillPoco();
                    item.Id = reader.GetGuid(0);
                    item.Job = reader.GetGuid(1);
                    item.Skill = reader.GetString(2);
                    item.SkillLevel = reader.GetString(3);
                    item.Importance = reader.GetInt32(4);
                    item.TimeStamp = (byte[])reader[5];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }


        public void Remove(params CompanyJobSkillPoco[] items)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobSkillPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Job_Skills]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }


        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                               SET 
                                                  [Job] = @Job
                                                  ,[Skill] =@Skill
                                                  ,[Skill_Level] = @Skill_Level
                                                  ,[Importance] = @Importance
                                            WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
