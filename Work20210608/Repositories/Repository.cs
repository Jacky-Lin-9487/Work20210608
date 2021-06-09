using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Data;
using Work20210608.Interfaces;

namespace Work20210608.Repositories
{
    public class Repository : IRepository
    {
        private readonly DbContext _dbContext;

        public Repository(Work20210608Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
