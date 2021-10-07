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
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {

        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleService()
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }

        public override Task<SecurityLoginsRolePayLoad> ReadSecurityLoginsRole(IdRequestLoginsRole request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRolePayLoad>(() => new SecurityLoginsRolePayLoad()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Role = poco.Role.ToString(),
            });
        }
        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            }
            };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            }
            };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco(){
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            }
            };
            _logic.Delete(pocos);
            return new Task<Empty>(() => new Empty());
        }

    }
}