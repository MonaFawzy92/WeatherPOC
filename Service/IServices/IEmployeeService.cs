using Domain.Entities;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.DTO;
using Util.Models;

namespace Service.IServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetEmployee(Guid id);
        Task<PagedResponseDTO<EmployeeDTO>> GetEmployees(PaginationFilter Filter);
        Task<Employee> AddEmployee(EmployeeDTO Employee);
    }
}
