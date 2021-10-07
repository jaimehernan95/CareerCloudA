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
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {

        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<CompanyJobSkillPayLoad> ReadCompanyJobSkill(IdRequestJobSkill request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillPayLoad>(() => new CompanyJobSkillPayLoad()
            {

                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel,
                Importance = poco.Importance

            });
        }
        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[] { new CompanyJobSkillPoco(){
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[] { new CompanyJobSkillPoco(){
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[] { new CompanyJobSkillPoco(){
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }

}