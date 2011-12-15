using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common.DataAccess
{
    public interface IEntity
    {
        int Id { get; }
    }

    public interface IRepository
    {
        T Get<T>(int id) where T : IEntity;
        T Get<T>(Expression<Func<T, bool>> predicate) where T : IEntity;

        IQueryable<T> Find<T>() where T : IEntity;
        IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : IEntity;

        T Add<T>(T entity) where T : IEntity;
        T Remove<T>(T entity) where T : IEntity;
    }
}