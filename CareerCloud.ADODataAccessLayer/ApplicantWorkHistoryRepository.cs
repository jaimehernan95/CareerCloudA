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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Company_Name]
                                   ,[Country_Code]
                                   ,[Location]
                                   ,[Job_Title]
                                   ,[Job_Description]
                                   ,[Start_Month]
                                   ,[Start_Year]
                                   ,[End_Month]
                                   ,[End_Year])
                             VALUES
                                   (@Id
                                   ,@Applicant
                                   ,@Company_Name
                                   ,@Country_Code
                                   ,@Location
                                   ,@Job_Title
                                   ,@Job_Description
                                   ,@Start_Month
                                   ,@Start_Year
                                   ,@End_Month
                                   ,@End_Year)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"
                             SELECT [Id]
                                  ,[Applicant]
                                  ,[Company_Name]
                                  ,[Country_Code]
                                  ,[Location]
                                  ,[Job_Title]
                                  ,[Job_Description]
                                  ,[Start_Month]
                                  ,[Start_Year]
                                  ,[End_Month]
                                  ,[End_Year]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Work_History]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantWorkHistoryPoco[] items = new ApplicantWorkHistoryPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco item = new ApplicantWorkHistoryPoco();
                    item.Id = reader.GetGuid(0);
                    item.Applicant = reader.GetGuid(1);
                    item.CompanyName = reader.GetString(2);
                    item.CountryCode = reader.GetString(3);
                    item.Location = reader.GetString(4);
                    item.JobTitle = reader.GetString(5);
                    item.JobDescription = reader.GetString(6);
                    item.StartMonth = reader.GetInt16(7);
                    item.StartYear = reader.GetInt32(8);
                    item.EndMonth = reader.GetInt16(9);
                    item.EndYear = reader.GetInt32(10);
                    item.TimeStamp = (byte[])reader[11];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }
        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Applicant_Work_History]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                       SET
                                          [Applicant] = @Applicant
                                          ,[Company_Name] = @Company_Name
                                          ,[Country_Code] = @Country_Code
                                          ,[Location] = @Location
                                          ,[Job_Title] = @Job_Title
                                          ,[Job_Description] = @Job_Description
                                          ,[Start_Month] = @Start_Month
                                          ,[Start_Year] = @Start_Year
                                          ,[End_Month] = @End_Month
                                          ,[End_Year] = @End_Year
                                          WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@Location", item.Location);
                    command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
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
