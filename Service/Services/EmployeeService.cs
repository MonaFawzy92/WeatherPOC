using AutoMapper;
using Domain.Entities;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.DTO;
using Service.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Util.DTO;
using Util.Models;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<EmployeeDTO> GetEmployee(Guid id)
        {
            Employee Emp = await _employeeRepository.GetEmp(id);
            return _mapper.Map<Employee, EmployeeDTO>(Emp);
        }
        public async Task<Employee> AddEmployee(EmployeeDTO employee)
        {
            Employee _employee = _mapper.Map<EmployeeDTO, Employee>(employee);
            Employee obj = await _employeeRepository.AddItemAsync(_employee);
            await _unitOfWork.Commit();
            return obj;
        }

        public async Task<PagedResponseDTO<EmployeeDTO>> GetEmployees(PaginationFilter Filter)
        {
            var EmployeesRecords = _employeeRepository.GetDataAsQuery();
            var PageRecords = await PagedResponse<Employee>.ToPagedList(EmployeesRecords, Filter);
            var Result = _mapper.Map<PagedResponseDTO<EmployeeDTO>>(PageRecords);
            return Result;
        }
    }
}
