﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work20210608.Interfaces
{
    public interface IRepository
    {
        void Create<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        IQueryable<T> GetAll<T>() where T : class;
    }
}
