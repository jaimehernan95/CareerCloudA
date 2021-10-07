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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {

        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<CompanyDescriptionPayLoad> ReadCompanyDescription(IdRequestDescription request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyDescriptionPayLoad>(() => new CompanyDescriptionPayLoad()
            {

                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                LanguageId = poco.LanguageId.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription

            });
        }
        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[] { new CompanyDescriptionPoco(){
                Id = Guid.Parse(request.Id),
                Company =  Guid.Parse(request.Company),
                LanguageId =  request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[] { new CompanyDescriptionPoco(){
                Id = Guid.Parse(request.Id),
                Company =  Guid.Parse(request.Company),
                LanguageId =  request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[] { new CompanyDescriptionPoco(){
                Id = Guid.Parse(request.Id),
                Company =  Guid.Parse(request.Company),
                LanguageId =  request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }
    }
}