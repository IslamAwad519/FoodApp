﻿using FoodApp.Api.Data.Entities;
using FoodApp.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data.Context;
using System.Linq.Expressions;

namespace FoodApp.Api.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _dBContext;

        public GenericRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dBContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var query = _dBContext.Set<T>().AsQueryable();
            query = query.Where(x => !x.IsDeleted);
            query = query.Where(expression);
            return await query.ToListAsync();
        }

        public async Task<IQueryable<T>> GetAsyncToInclude(Expression<Func<T, bool>> expression)
        {
            var query = _dBContext.Set<T>().AsQueryable();
            query = query.Where(x => !x.IsDeleted);
            query = query.Where(expression);
            return  query;
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dBContext.Set<T>().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dBContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dBContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void DeleteById(int id)
        {
            T entity = _dBContext.Find<T>(id)!;
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task<int> GetCountAsync()
        {
            return await _dBContext.Set<T>().Where(x => !x.IsDeleted).CountAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _dBContext.Set<T>().Where(a => a.IsDeleted != true);
            return query;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll(predicate).FirstOrDefaultAsync();
        }


 
    }
}
