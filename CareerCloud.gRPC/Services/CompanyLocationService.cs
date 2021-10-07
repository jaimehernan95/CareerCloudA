using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {

        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService()
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        public override Task<CompanyLocationPayLoad> ReadCompanyLocation(IdRequestLocation request, ServerCallContext context)
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyLocationPayLoad>(() => new CompanyLocationPayLoad()
            {

                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CountryCode = poco.CountryCode,
                StateProvinceCode = poco.Province,
                StreetAddress = poco.Street,
                CityTown = poco.City,
                ZipPostalCode = poco.PostalCode
            });
        }
        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[] { new CompanyLocationPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CountryCode = request.CountryCode,
                Province = request.StateProvinceCode,
                Street = request.StreetAddress,
                City = request.CityTown,
                PostalCode = request.ZipPostalCode
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[] { new CompanyLocationPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CountryCode = request.CountryCode,
                Province = request.StateProvinceCode,
                Street = request.StreetAddress,
                City = request.CityTown,
                PostalCode = request.ZipPostalCode
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[] { new CompanyLocationPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CountryCode = request.CountryCode,
                Province = request.StateProvinceCode,
                Street = request.StreetAddress,
                City = request.CityTown,
                PostalCode = request.ZipPostalCode
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }
    }
}