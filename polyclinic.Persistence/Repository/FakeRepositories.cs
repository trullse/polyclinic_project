using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Persistence.Repository
{
    public class FakeClientRepository : IRepository<Client>
    {
        List<Client> _clients;
        public FakeClientRepository()
        {
            _clients = new List<Client>()
            {
                new Client()
                {
                    Id=1, Name="Alex", Surname="Leon", BirthDate=DateTime.Now.AddYears(-18)
                },
                new Client()
                {
                    Id=2, Name="Olga", Surname="Smith", BirthDate=DateTime.Now.AddYears(-25)
                }
            };
        }

        public Task AddAsync(Client entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Client entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> FirstOrDefaultAsync(Expression<Func<Client, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Client, object>>[] includesProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Client>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _clients);
        }

        public Task<IReadOnlyList<Client>> ListAsync(Expression<Func<Client, bool>>? filter, CancellationToken cancellationToken = default, params Expression<Func<Client, object>>[] includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeAppointmentRepository : IRepository<Appointment>
    {
        List<Appointment> _list = new List<Appointment>();
        public FakeAppointmentRepository()
        {
            Random rand = new Random();
            int k = 1;
            for (int i = 1; i <= 2; i++)
                for (int j = 0; j < 10; j++)
                    _list.Add(new Appointment()
                    {
                        Id = k,
                        Diagnosis = $"Diagnosis {k++}",
                        ClientId = i,
                        DoctorId = 1,
                        AppointmentDate = DateTime.Now.AddDays(rand.NextInt64()),
                        TreatmentCost = rand.NextDouble() * 10
                    });
        }

        public Task AddAsync(Appointment entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Appointment entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment?> FirstOrDefaultAsync(Expression<Func<Appointment, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Appointment, object>>[] includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Appointment>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Appointment>> ListAsync(
            Expression<Func<Appointment, bool>>? filter, 
            CancellationToken cancellationToken = default, 
            params Expression<Func<Appointment, object>>[] includesProperties)
        {
            var data = _list.AsQueryable();
            return data.Where(filter).ToList();
        }

        public Task UpdateAsync(Appointment entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
