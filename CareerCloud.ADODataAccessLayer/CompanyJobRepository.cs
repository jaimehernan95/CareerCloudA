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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyJobRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                                   ([Id]
                                                   ,[Company]
                                                   ,[Profile_Created]
                                                   ,[Is_Inactive]
                                                   ,[Is_Company_Hidden])
                                             VALUES
                                                   (@Id
                                                   ,@Company
                                                   ,@Profile_Created
                                                   ,@Is_Inactive
                                                   ,@Is_Company_Hidden)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @" SELECT [Id]
                                  ,[Company]
                                  ,[Profile_Created]
                                  ,[Is_Inactive]
                                  ,[Is_Company_Hidden]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Jobs]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyJobPoco[] items = new CompanyJobPoco[1500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobPoco item = new CompanyJobPoco();
                    item.Id = reader.GetGuid(0);
                    item.Company = reader.GetGuid(1);
                    item.ProfileCreated = reader.GetDateTime(2);
                    item.IsInactive = reader.GetBoolean(3);
                    item.IsCompanyHidden = reader.GetBoolean(4);
                    item.TimeStamp = (byte[])reader[5];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyJobPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Jobs]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }


        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                       SET 
                                          [Company] = @Company
                                          ,[Profile_Created] =@Profile_Created
                                          ,[Is_Inactive] = @Is_Inactive
                                          ,[Is_Company_Hidden] = @Is_Company_Hidden
                                            WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
