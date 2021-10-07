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
using static CareerCloud.gRPC.Protos.ApplicantResume;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantResumeService : ApplicantResumeBase
    {

        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeService()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        public override Task<ApplicantResumePayLoad> ReadApplicantResume(IdRequestResume request, ServerCallContext context)
        {
            ApplicantResumePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantResumePayLoad>(() => new ApplicantResumePayLoad()
            {

                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Resume = poco.Resume,
                LastUpdated = poco.LastUpdated is null ? null : Timestamp.FromDateTime((DateTime)poco.LastUpdated),

            });
        }
        public override Task<Empty> CreateApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[] { new ApplicantResumePoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Resume = request.Resume,
                LastUpdated = request.LastUpdated.ToDateTime()

                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[] { new ApplicantResumePoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Resume = request.Resume,
                LastUpdated = request.LastUpdated.ToDateTime()

                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[] { new ApplicantResumePoco(){
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Resume = request.Resume,
                LastUpdated = request.LastUpdated.ToDateTime()

                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}