using EmployeeWebApi.Data;
using EmployeeWebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApi.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public  async Task<Department> GetDepartment(int departmentId)
        {
            return appDbContext.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await appDbContext.Departments.ToListAsync();
        }
    }
}
