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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddAsync(Client item)
        {
            _unitOfWork.ClientRepository.AddAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task DeleteAsync(Client item)
        {
            _unitOfWork.ClientRepository.DeleteAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task<IReadOnlyList<Client>> GetAllAsync()
        {
            return _unitOfWork.ClientRepository.ListAllAsync();
        }

        public Task<Client?> GetByIdAsync(int id)
        {
            return _unitOfWork.ClientRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Client item)
        {
            _unitOfWork.ClientRepository.UpdateAsync(item);
            return _unitOfWork.SaveAllAsync();
        }
    }
}
