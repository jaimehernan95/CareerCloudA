using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{

    public class ApplicantEducationService : ApplicantEducationBase
    {


        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        public override Task<ApplicantEducationPayLoad> ReadApplicantEducation(IdRequestEducation request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            return new Task<ApplicantEducationPayLoad>(() => new ApplicantEducationPayLoad()
            {

                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CertficateDiploma = poco.CertificateDiploma,
                CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercentage = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent,
                Major = poco.Major,
                StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate)
            });
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[] { new ApplicantEducationPoco(){
             Id = Guid.Parse(request.Id),
             Applicant = Guid.Parse(request.Applicant),
            CertificateDiploma = request.CertficateDiploma,
            CompletionDate = request.CompletionDate.ToDateTime(),
            CompletionPercent = (byte)request.CompletionPercentage,
            Major = request.Major,
            StartDate = request.StartDate.ToDateTime(),
                }
             };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            //ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[] { new ApplicantEducationPoco(){
            Id = Guid.Parse(request.Id),
            Applicant = Guid.Parse(request.Applicant),
            CertificateDiploma = request.CertficateDiploma,
            CompletionDate = request.CompletionDate.ToDateTime(),
            CompletionPercent = (byte)request.CompletionPercentage,
            Major = request.Major,
            StartDate = request.StartDate.ToDateTime(),
                }
             };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {

            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[] { new ApplicantEducationPoco(){
             Id = Guid.Parse(request.Id),
             Applicant = Guid.Parse(request.Applicant),
            CertificateDiploma = request.CertficateDiploma,
            CompletionDate = request.CompletionDate.ToDateTime(),
            CompletionPercent = (byte)request.CompletionPercentage,
            Major = request.Major,
            StartDate = request.StartDate.ToDateTime(),
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }

}