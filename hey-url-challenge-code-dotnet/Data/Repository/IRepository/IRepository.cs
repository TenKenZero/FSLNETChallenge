using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace hey_url_challenge_code_dotnet.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            String includeProperties = null
        );

        T GetFirstorDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
    }
}
