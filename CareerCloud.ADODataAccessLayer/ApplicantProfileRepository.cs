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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string _connStr;
        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                       ([Id]
                                       ,[Login]
                                       ,[Current_Salary]
                                       ,[Current_Rate]
                                       ,[Currency]
                                       ,[Country_Code]
                                       ,[State_Province_Code]
                                       ,[Street_Address]
                                       ,[City_Town]
                                       ,[Zip_Postal_Code])
                                 VALUES
                                      (
                                       @Id
                                       ,@Login
                                       ,@Current_Salary
                                       ,@Current_Rate
                                       ,@Currency
                                       ,@Country_Code
                                       ,@State_Province_Code
                                       ,@Street_Address
                                       ,@City_Town
                                       ,@Zip_Postal_Code)";


                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    comm.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    comm.Parameters.AddWithValue("@Currency", poco.Currency);
                    comm.Parameters.AddWithValue("@Country_Code", poco.Country);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Current_Salary]
                                  ,[Current_Rate]
                                  ,[Currency]
                                  ,[Country_Code]
                                  ,[State_Province_Code]
                                  ,[Street_Address]
                                  ,[City_Town]
                                  ,[Zip_Postal_Code]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Profiles]";
                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.GetDecimal(2);
                    poco.CurrentRate = reader.GetDecimal(3);
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];


                    pocos[index] = poco;
                    index++;

                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"DELETE FROM[dbo].[Applicant_Profiles]
                                   WHERE  [Id]= @Id";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (var poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                       SET 
                                           [Login] = @Login
                                          ,[Current_Salary] =@Current_Salary
                                          ,[Current_Rate] = @Current_Rate
                                          ,[Currency] = @Currency
                                          ,[Country_Code] = @Country_Code
                                          ,[State_Province_Code] = @State_Province_Code
                                          ,[Street_Address] = @Street_Address
                                          ,[City_Town] = @City_Town
                                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                                           WHERE  [Id]= @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    comm.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    comm.Parameters.AddWithValue("@Currency", poco.Currency);
                    comm.Parameters.AddWithValue("@Country_Code", poco.Country);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }
    }
}