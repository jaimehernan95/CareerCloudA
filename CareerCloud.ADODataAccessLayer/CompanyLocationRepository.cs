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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string _connStr;
        /*SqlConnection _sqlcon;*/
        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            /*_sqlcon = new SqlConnection(_connStr);*/
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyLocationPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                           ([Id]
                                           ,[Company]
                                           ,[Country_Code]
                                           ,[State_Province_Code]
                                           ,[Street_Address]
                                           ,[City_Town]
                                           ,[Zip_Postal_Code])
                                     VALUES
                                           (@Id
                                           ,@Company
                                           ,@Country_Code
                                           ,@State_Province_Code
                                           ,@Street_Address
                                           ,@City_Town
                                           ,@Zip_Postal_Code)";


                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {

            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = _sqlcon;
                command.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[Country_Code]
                                  ,[State_Province_Code]
                                  ,[Street_Address]
                                  ,[City_Town]
                                  ,[Zip_Postal_Code]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Locations]";
                _sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                CompanyLocationPoco[] items = new CompanyLocationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco item = new CompanyLocationPoco();
                    item.Id = reader.GetGuid(0);
                    item.Company = reader.GetGuid(1);
                    item.CountryCode = reader.GetString(2);
                    item.Province = reader.GetString(3);
                    item.Street = reader.GetString(4);
                    item.City = reader.IsDBNull(5) ? (string)null : reader.GetString(5);
                    item.PostalCode = reader.IsDBNull(6) ? (string)null : reader.GetString(6);

                    item.TimeStamp = (byte[])reader[7];

                    items[index] = item;
                    index++;

                }
                _sqlcon.Close();
                return items.Where(a => a != null).ToList();
            }
        }


        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> items = GetAll().AsQueryable();
            return items.Where(where).FirstOrDefault();
        }


        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (CompanyLocationPoco item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"DELETE FROM[dbo].[Company_Locations]
                                   WHERE  [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection _sqlcon = new SqlConnection(_connStr))
            {
                foreach (var item in items)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = _sqlcon;
                    command.CommandText = @"UPDATE [dbo].[Company_Locations]
                                       SET 
                                          [Company] = @Company
                                          ,[Country_Code] = @Country_Code
                                          ,[State_Province_Code] = @State_Province_Code
                                          ,[Street_Address] = @Street_Address
                                          ,[City_Town] = @City_Town
                                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                                            WHERE  [Id]= @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    _sqlcon.Open();
                    command.ExecuteNonQuery();
                    _sqlcon.Close();

                }
            }
        }
    }
}
