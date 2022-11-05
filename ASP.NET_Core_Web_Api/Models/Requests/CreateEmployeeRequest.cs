namespace ASP.NET_Core_Web_Api.Models.Requests
{
    public class CreateEmployeeRequest
    {
        public int DepartmentId { get; set; }
        public int EmployeeTypeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public decimal Salary { get; set; }
    }
}
