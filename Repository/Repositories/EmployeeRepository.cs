using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly IUnitOfWork UnitOfWork;

        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Employee> GetEmp(Guid id)
        {
            return await UnitOfWork.Context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
