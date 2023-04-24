using Microsoft.EntityFrameworkCore;
using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using polyclinic.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Persistence.Repository
{
    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entities;
        public EfRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entities.Add(entity);
            //_context.SaveChangesAsync(cancellationToken: cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                if (_entities.Contains(entity))
                {
                    _entities.Remove(entity);
                    //_context.SaveChangesAsync(cancellationToken: cancellationToken);
                }
            }
            return Task.CompletedTask;
        }

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<T>? query = _entities.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public Task<T?> GetByIdAsync(
            int id, 
            CancellationToken cancellationToken = default, 
            params Expression<Func<T, object>>[] includesProperties)
        {
            IQueryable<T>? query = _entities.AsQueryable();
            if (includesProperties.Any())
            {
                foreach (Expression<Func<T, object>>? included in
                includesProperties)
                {
                    query = query.Include(included);
                }
            }
            return query.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await _entities.ToListAsync(cancellationToken: cancellationToken);
            return list;
        }

        public async Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>>? filter, 
            CancellationToken cancellationToken = default, 
            params Expression<Func<T, object>>[] includesProperties)
        {
            IQueryable<T>? query = _entities.AsQueryable();
            if (includesProperties.Any())
            {
                foreach (Expression<Func<T, object>>? included in
                includesProperties)
                {
                    query = query.Include(included);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_context.SaveChangesAsync(cancellationToken: cancellationToken);
            return Task.CompletedTask;
        }
    }
}