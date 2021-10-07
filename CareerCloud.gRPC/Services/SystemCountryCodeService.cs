namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeService()
        {
            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        public override Task<SystemCountryCodePayLoad> ReadSystemCountryCode(IdRequestCountryCode request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Code);
            return new Task<SystemCountryCodePayLoad>(() => new SystemCountryCodePayLoad()
            {

                Code = poco.Code,
                Name = poco.Name
            });
        }
        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[] { new SystemCountryCodePoco(){
                 Code = request.Code,
                Name = request.Name
            }
            };
            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[] { new SystemCountryCodePoco(){
                Code = request.Code,
                Name = request.Name
            }
            };
            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[] { new SystemCountryCodePoco(){
                Code = request.Code,
                Name = request.Name
            }
            };
            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }
    }
}