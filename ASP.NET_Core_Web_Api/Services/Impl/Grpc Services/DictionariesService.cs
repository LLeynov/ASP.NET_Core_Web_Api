using ASP.NET_Core_Web_Api.Models.Requests;
using ASPNETCoreWebApiProto;
using EmployeeService.Data;
using Grpc.Core;
using static ASPNETCoreWebApiProto.DictionariesService;
using EmployeeType = ASPNETCoreWebApiProto.EmployeeType;

namespace ASP.NET_Core_Web_Api.Services.Impl.Grpc_Services
{
    public class DictionariesService : DictionariesServiceBase
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DictionariesService(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public override Task<CreateEmployeeTypeResponse> CreateEmployeeType(CreateEmployeeTypeRequest request, ServerCallContext context)
        {
            var id = _employeeTypeRepository.Create(new EmployeeService.Data.EmployeeType
            {
                Description = request.Description
            });

            CreateEmployeeTypeResponse response = new CreateEmployeeTypeResponse();
            response.Id = id;
            return Task.FromResult(response);
        }

        public override Task<DeleteEmployeeTypeResponse> DeleteEmployeeType(DeleteEmployeeTypeRequest request, ServerCallContext context)
        {
            _employeeTypeRepository.Delete(request.Id);
            return Task.FromResult(new DeleteEmployeeTypeResponse());
        }

        public override Task<GetAllEmployeeTypesResponse> GetAllEmployeeTypes(GetAllEmployeeTypesRequest request, ServerCallContext context)
        {
            GetAllEmployeeTypesResponse response = new GetAllEmployeeTypesResponse();

            response.EmployeeTypes.AddRange(_employeeTypeRepository.GetAll().Select(et =>
                new ASPNETCoreWebApiProto.EmployeeType
                {
                    Id = et.Id,
                    Description = et.Description
                }).ToList());

            return Task.FromResult(response);
        }
    }
}
