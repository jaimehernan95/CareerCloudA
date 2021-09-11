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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyProfilePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id]
                                       ,[Registration_Date]
                                       ,[Company_Website]
                                       ,[Contact_Phone]
                                       ,[Contact_Name]
                                       ,[Company_Logo])
                                 VALUES
                                       (@Id
                                       ,@Registration_Date
                                       ,@Company_Website
                                       ,@Contact_Phone
                                       ,@Contact_Name
                                       ,@Company_Logo)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);


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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                      ,[Registration_Date]
                                      ,[Company_Website]
                                      ,[Contact_Phone]
                                      ,[Contact_Name]
                                      ,[Company_Logo]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Profiles]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyProfilePoco[] items = new CompanyProfilePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco item = new CompanyProfilePoco();
                    item.Id = reader.GetGuid(0);
                    item.RegistrationDate = reader.GetDateTime(1);
                    item.CompanyWebsite = reader.IsDBNull(2) ? (string)null : reader.GetString(2);
                    item.ContactPhone = reader.GetString(3);
                    item.ContactName = reader.IsDBNull(4) ? (string)null : reader.GetString(4); ;

                    item.CompanyLogo = reader.IsDBNull(5) ? (byte[])null : (byte[])reader[5];
                    item.TimeStamp = (byte[])reader[6];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyProfilePoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Profiles]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                           SET 
                                              [Registration_Date] = @Registration_Date
                                              ,[Company_Website] = @Company_Website
                                              ,[Contact_Phone] = @Contact_Phone
                                              ,[Contact_Name] = @Contact_Name
                                              ,[Company_Logo] = @Company_Logo
                                            WHERE  [Id]= @Id";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
