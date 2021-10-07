using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {

        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<CompanyProfilePayLoad> ReadCompanyProfile(IdRequestCompanyProfile request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfilePayLoad>(() => new CompanyProfilePayLoad()
            {

                Id = poco.Id.ToString(),

                RegistrationDate = Timestamp.FromDateTime((DateTime)poco.RegistrationDate),
                CompanyWebsite = poco.CompanyWebsite,
                ContactPhone = poco.ContactPhone,
                ContactName = poco.ContactName,
                CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)

            });
        }
        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[] { new CompanyProfilePoco(){
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[] { new CompanyProfilePoco(){
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[] { new CompanyProfilePoco(){
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}