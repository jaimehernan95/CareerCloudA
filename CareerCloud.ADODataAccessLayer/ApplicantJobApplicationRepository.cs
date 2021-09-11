using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connStr;
        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO[dbo].[Applicant_Job_Applications]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Job]
                                               ,[Application_Date])
                                         VALUES
                                               (@Id
                                               ,@Applicant
                                               ,@Job
                                               ,@Application_Date)";


                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);


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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Job]
                                  ,[Application_Date]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Job_Applications]";
                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[index] = poco;
                    index++;

                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"DELETE FROM[dbo].[Applicant_Job_Applications]
                                   WHERE  [Id]= @Id";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (var poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                       SET 
                                           [Applicant] = @Applicant
                                          ,[Job] = @Job
                                          ,[Application_Date] = @Application_Date
                                          WHERE  [Id]= @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();

                }

            }
        }
    }
}