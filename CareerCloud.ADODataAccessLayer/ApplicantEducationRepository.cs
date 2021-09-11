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
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string _connStr;
        public ApplicantEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantEducationPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO[dbo].[Applicant_Educations]
                                   ([Id]
                                   ,[Applicant]
                                   ,[Major]
                                   ,[Certificate_Diploma]
                                   ,[Start_Date]
                                   ,[Completion_Date]
                                   ,[Completion_Percent])
                             VALUES
                                 (
                                    @Id
                                   ,@Applicant
                                   ,@Major
                                   ,@Certificate_Diploma
                                   ,@Start_Date
                                   ,@Completion_Date
                                   ,@Completion_Percent 
                                  )";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    comm.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    comm.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    comm.Parameters.AddWithValue("@Completion_Percent ", poco.CompletionPercent);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();

                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {

            using (SqlConnection connection = new SqlConnection(_connStr))
            {

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Major]
                                      ,[Certificate_Diploma]
                                      ,[Start_Date]
                                      ,[Completion_Date]
                                      ,[Completion_Percent]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Educations]";
                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = reader.GetDateTime(4);
                    poco.CompletionDate = reader.GetDateTime(5);
                    poco.CompletionPercent = reader.GetByte(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[index] = poco;
                    index++;

                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }



        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"DELETE FROM[dbo].[Applicant_Educations]
                                   WHERE  [Id]= @Id";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (var poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                           SET 
                                               [Applicant] = @Applicant
                                              ,[Major] = @Major
                                              ,[Certificate_Diploma] = @Certificate_Diploma
                                              ,[Start_Date] = @Start_Date
                                              ,[Completion_Date] = @Completion_Date
                                              ,[Completion_Percent] = @Completion_Percent
                                         WHERE  [Id]= @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    comm.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    comm.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    comm.Parameters.AddWithValue("@Completion_Percent ", poco.CompletionPercent);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();

                }

            }
        }

    }
}