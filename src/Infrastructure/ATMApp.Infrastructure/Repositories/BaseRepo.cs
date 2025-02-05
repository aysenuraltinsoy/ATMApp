﻿using ATMApp.Domain.Entities;
using ATMApp.Domain.Repositories;
using ATMApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Infrastructure.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseEntity
    {
        private readonly ATMAppDbContext _atmAppDbContext;
        protected DbSet<T> table;

        public BaseRepo(ATMAppDbContext atmAppDbContext)
        {
            _atmAppDbContext = atmAppDbContext;
            table = _atmAppDbContext.Set<T>();
        }
        public async Task<bool> Any(Expression<Func<T, bool>> exception)
        {
            return await table.AnyAsync(exception);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
            await _atmAppDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await _atmAppDbContext.SaveChangesAsync();
        }



        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _atmAppDbContext.Entry<T>(entity).State = EntityState.Modified;
            await _atmAppDbContext.SaveChangesAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select,
                                                                      Expression<Func<T, bool>> where,
                                                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;



            if (where != null)
            {
                query = query.Where(where);
            }



            if (include != null)
            {
                query = include(query);
            }



            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(select).FirstOrDefaultAsync();
            }



        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select,
                                                                  Expression<Func<T, bool>> where,
                                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;



            if (where != null)
            {
                query = query.Where(where);
            }



            if (include != null)
            {
                query = include(query);
            }



            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
            {
                return await query.Select(select).ToListAsync();
            }
        }
    }
}
