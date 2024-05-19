using Domain.Entities;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DataPurgeService : IDataPurgeService
    {

        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _currentDataPurgeDurationInDays;

        public DataPurgeService(IGenericRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _currentDataPurgeDurationInDays = 365;
        }


        public void DeleteOldData()
        {
            var currentDate = DateTime.Now;
            var toBeDeletedEmployees = _employeeRepository.GetFilteredData(t => (currentDate - t.CreatedOn).Days == _currentDataPurgeDurationInDays);
            _employeeRepository.RemoveRange(toBeDeletedEmployees);
            _unitOfWork.Commit();
        }


    }
}
