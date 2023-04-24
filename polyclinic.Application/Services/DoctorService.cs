using polyclinic.Application.Abstractions;
using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddAsync(Doctor item)
        {
            _unitOfWork.DoctorRepository.AddAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task DeleteAsync(Doctor item)
        {
            _unitOfWork.DoctorRepository.DeleteAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task<IReadOnlyList<Doctor>> GetAllAsync()
        {
            return _unitOfWork.DoctorRepository.ListAllAsync();
        }

        public Task<Doctor?> GetByIdAsync(int id)
        {
            return _unitOfWork.DoctorRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Doctor item)
        {
            _unitOfWork.DoctorRepository.UpdateAsync(item);
            return _unitOfWork.SaveAllAsync();
        }
    }
}
