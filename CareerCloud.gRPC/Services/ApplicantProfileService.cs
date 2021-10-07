using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {

        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<ApplicantProfilePayLoad> ReadApplicantProfile(IdRequestProfile request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantProfilePayLoad>(() => new ApplicantProfilePayLoad()
            {

                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                CurrentSalary = (double)poco.CurrentSalary,
                CurrentRate = (double)poco.CurrentRate,
                Currency = poco.Currency,
                CountryCode = poco.Country,
                StateProvinceCode = poco.Province,
                StreetAddress = poco.Street,
                CityTown = poco.City,
                ZipPostalCode = poco.PostalCode

            });
        }
        public override Task<Empty> CreateApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[] { new ApplicantProfilePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.CountryCode,
                Province = request.StateProvinceCode,
                Street = request.StreetAddress,
                City = request.CityTown,
                PostalCode = request.ZipPostalCode

                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[] { new ApplicantProfilePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.CountryCode,
                Province = request.StateProvinceCode,
                Street = request.StreetAddress,
                City = request.CityTown,
                PostalCode = request.ZipPostalCode

                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[] { new ApplicantProfilePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.CountryCode,
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