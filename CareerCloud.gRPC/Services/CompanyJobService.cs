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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<CompanyJobPayLoad> ReadCompanyJob(IdRequestJob request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobPayLoad>(() => new CompanyJobPayLoad()
            {

                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                ProfileCreated = Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                IsInactive = poco.IsInactive,
                IsCompanyHidden = poco.IsCompanyHidden

            });
        }
        public override Task<Empty> CreateCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[] { new CompanyJobPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[] { new CompanyJobPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[] { new CompanyJobPoco(){
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }
    }
}