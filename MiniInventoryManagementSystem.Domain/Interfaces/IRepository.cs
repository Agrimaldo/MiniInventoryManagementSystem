﻿using System.Linq.Expressions;

namespace MiniInventoryManagementSystem.Domain.Interfaces

{
    public interface IRepository
    {

        T? Add<T>(T obj) where T : class;
        T? Update<T>(T obj) where T : class;
        bool Delete<T>(T obj) where T : class;
        List<T>? List<T>(int skip = 0, int take = 50, Expression<Func<T, bool>>? conditional = null) where T : class;
    }
}
