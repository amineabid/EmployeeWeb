using EmployeeManagement.Models;

namespace EmployeeManagement.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<HttpResponseMessage> UpdateEmployee(Employee updatedEmployee);
    }
}
