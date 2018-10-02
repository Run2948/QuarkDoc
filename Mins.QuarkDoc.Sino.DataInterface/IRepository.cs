using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mins.QuarkDoc.DataInterface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entiy);

        void Update(TEntity entity);

        void UpdateField(TEntity entiy, params string[] paramField);

        void Delete(TEntity entity);
        void Delete(ISpecification<TEntity> speicification);

        TEntity FirstOrDefault(ISpecification<TEntity> specification);

        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification, bool includeDefalutObjects = false, List<string> includeObjects = null);
        int GetPagedCount(ISpecification<TEntity> specification);

        IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, List<string> includeObjects, ISpecification<TEntity> specification, Expression<Func<TEntity, S>> orderByExpression, bool ascending, int pageSize = 10);
        IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, ISpecification<TEntity> specification, Expression<Func<TEntity, S>> orderByExpression, bool ascending, int pageSize = 10);
    }
}
