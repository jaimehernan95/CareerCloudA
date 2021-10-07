using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {


        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        public override Task<ApplicantJobApplicationPayLoad> ReadApplicantJobApplication(IdRequestJobApplication request, ServerCallContext context)
        {

            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantJobApplicationPayLoad>(() => new ApplicantJobApplicationPayLoad()
            {

                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = Timestamp.FromDateTime((DateTime)poco.ApplicationDate),

            });
        }


        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco(){
            Id = Guid.Parse(request.Id),
            Applicant = Guid.Parse(request.Applicant),
            Job = Guid.Parse(request.Job),
            ApplicationDate = request.ApplicationDate.ToDateTime()

                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco(){
            Id = Guid.Parse(request.Id),
            Applicant = Guid.Parse(request.Applicant),
            Job = Guid.Parse(request.Job),
            ApplicationDate = request.ApplicationDate.ToDateTime()

                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco(){
            Id = Guid.Parse(request.Id),
            Applicant = Guid.Parse(request.Applicant),
            Job = Guid.Parse(request.Job),
            ApplicationDate = request.ApplicationDate.ToDateTime()

                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}