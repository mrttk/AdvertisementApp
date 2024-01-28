using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AdvertisementContext _context;

        public Repository(AdvertisementContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return
                orderByType == OrderByType.ASC ?
                await _context.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() :
                await _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ?
                await _context.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() :
                await _context.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
    }
}
