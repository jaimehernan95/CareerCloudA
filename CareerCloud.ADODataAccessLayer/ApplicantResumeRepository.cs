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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/

        public ApplicantResumeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantResumePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Resume]
                                           ,[Last_Updated])
                                     VALUES
                                           (@Id
                                           ,@Applicant
                                           ,@Resume
                                           ,@Last_Updated)";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"
                                    SELECT [Id]
                                          ,[Applicant]
                                          ,[Resume]
                                          ,[Last_Updated]
                                      FROM [dbo].[Applicant_Resumes]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicantResumePoco[] items = new ApplicantResumePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantResumePoco item = new ApplicantResumePoco();
                    item.Id = reader.GetGuid(0);
                    item.Applicant = reader.GetGuid(1);
                    item.Resume = reader.GetString(2);
                    item.LastUpdated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }


        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (ApplicantResumePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Applicant_Resumes]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                       SET
                                          [Applicant] = @Applicant
                                          ,[Resume] = @Resume
                                          ,[Last_Updated] = @Last_Updated
                                          WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
