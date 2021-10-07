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
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {

        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillService()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        public override Task<ApplicantSkillPayLoad> ReadApplicantSkill(IdRequestSkill request, ServerCallContext context)
        {
            ApplicantSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantSkillPayLoad>(() => new ApplicantSkillPayLoad()
            {

                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel,
                StartMonth = poco.StartMonth,
                StartYear = poco.StartYear,
                EndMonth = poco.EndMonth,
                EndYear = poco.EndMonth
            });
        }
        public override Task<Empty> CreateApplicantSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[] { new ApplicantSkillPoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (byte)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (byte)request.EndMonth,
                EndYear = (byte)request.EndYear
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[] { new ApplicantSkillPoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (byte)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (byte)request.EndMonth,
                EndYear = (byte)request.EndYear
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[] { new ApplicantSkillPoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (byte)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (byte)request.EndMonth,
                EndYear = (byte)request.EndYear
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}