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
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {

        private readonly SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        public override Task<SecurityLoginPayLoad> ReadSecurityLogin(IdRequestLogin request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginPayLoad>(() => new SecurityLoginPayLoad()
            {

                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Password = poco.Password,
                CreatedDate = Timestamp.FromDateTime((DateTime)poco.Created),
                PasswordUpdateDate = poco.PasswordUpdate is null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                AgreementAcceptedDate = poco.AgreementAccepted is null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage
            });
        }
        public override Task<Empty> CreateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[] { new SecurityLoginPoco(){
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.CreatedDate.ToDateTime(),
                PasswordUpdate = request.PasswordUpdateDate.ToDateTime(),
                AgreementAccepted = request.PasswordUpdateDate.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword =request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage
                }
             };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[] { new SecurityLoginPoco(){
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.CreatedDate.ToDateTime(),
                PasswordUpdate = request.PasswordUpdateDate.ToDateTime(),
                AgreementAccepted = request.PasswordUpdateDate.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword =request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage
                }
             };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[] { new SecurityLoginPoco(){
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.CreatedDate.ToDateTime(),
                PasswordUpdate = request.PasswordUpdateDate.ToDateTime(),
                AgreementAccepted = request.PasswordUpdateDate.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword =request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage
                }
             };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

    }
}