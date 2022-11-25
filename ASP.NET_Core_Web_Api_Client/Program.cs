
using ASPNETCoreWebApiProto;
using Grpc.Net.Client;

namespace ASP.NET_Core_Web_Api_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            DictionariesService.DictionariesServiceClient client =
                new DictionariesService.DictionariesServiceClient(channel);

            Console.WriteLine("Укажите тип сотрудника.");
            
            var response = client.CreateEmployeeType(new CreateEmployeeTypeRequest
            {
                Description = Console.ReadLine()
            });

            if (response != null)
            {
                Console.WriteLine($"Тип сотрудника успешно добавлен #{response.Id}");
            }

            var getAllEmployeeTypesResponse = client.GetAllEmployeeTypes(new GetAllEmployeeTypesRequest());
            foreach (var employeeType in getAllEmployeeTypesResponse.EmployeeTypes)
            {
                Console.WriteLine($"#{employeeType.Id}/{employeeType.Description}");
            }

            Console.ReadKey(true);
        }
    }
}