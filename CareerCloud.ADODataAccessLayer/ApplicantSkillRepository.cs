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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string _connStr;
       /* SqlConnection _sqlcon;
*/
        public ApplicantSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantSkillPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Skill]
                                   ,[Skill_Level]
                                   ,[Start_Month]
                                   ,[Start_Year]
                                   ,[End_Month]
                                   ,[End_Year])
                             VALUES
                                   (@Id
                                   ,@Applicant
                                   ,@Skill
                                   ,@Skill_Level
                                   ,@Start_Month
                                   ,@Start_Year
                                   ,@End_Month
                                   ,@End_Year)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", item.EndYear);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"
                                    SELECT [Id]
                                  ,[Applicant]
                                  ,[Skill]
                                  ,[Skill_Level]
                                  ,[Start_Month]
                                  ,[Start_Year]
                                  ,[End_Month]
                                  ,[End_Year]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Skills]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantSkillPoco[] items = new ApplicantSkillPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantSkillPoco item = new ApplicantSkillPoco();
                    item.Id = reader.GetGuid(0);
                    item.Applicant = reader.GetGuid(1);
                    item.Skill = reader.GetString(2);
                    item.SkillLevel = reader.GetString(3);
                    item.StartMonth = reader.GetByte(4);
                    item.StartYear = reader.GetInt32(5);
                    item.EndMonth = reader.GetByte(6);
                    item.EndYear = reader.GetInt32(7);
                    item.TimeStamp = (byte[])reader[8];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }


        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantSkillPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Applicant_Skills]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                       SET 
                                          [Applicant] = @Applicant
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] =@Skill_Level
                                          ,[Start_Month] =@Start_Month
                                          ,[Start_Year] =@Start_Year
                                          ,[End_Month] =@End_Month
                                          ,[End_Year] =@End_Year
                                          WHERE  [Id]=@Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", item.EndYear);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
