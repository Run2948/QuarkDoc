using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Mins.QuarkDoc.BusinessService
{
    public class DbServiceBase<TEntity> where TEntity : class
    {
        #region 基础公共操作方法


        #region Expression

        public void Insert(TEntity entiy)
        {
            ContainerManager<TEntity>.GetResolve().Insert(entiy);
        }

        public void Update(TEntity entiy)
        {
            ContainerManager<TEntity>.GetResolve().Update(entiy);
        }

        public void UpdateField(TEntity entiy, string[] paramField)
        {
            ContainerManager<TEntity>.GetResolve().UpdateField(entiy, paramField);
        }

        public void Delete(TEntity entiy)
        {
            ContainerManager<TEntity>.GetResolve().Delete(entiy);
        }

        public void Delete(Expression<Func<TEntity, bool>> exception)
        {
            ContainerManager<TEntity>.GetResolve().Delete(new DirectSpecification<TEntity>(exception));
        }

        public IEnumerable<TEntity> FindAll()
        {
            return ContainerManager<TEntity>.GetResolve().FindAll();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> exception)
        {
            return ContainerManager<TEntity>.GetResolve().FindAll(new DirectSpecification<TEntity>(exception));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> exception)
        {
            return ContainerManager<TEntity>.GetResolve().FirstOrDefault(new DirectSpecification<TEntity>(exception));

        }

        public IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, ISpecification<TEntity> specification, Expression<Func<TEntity, S>> orderByExpression, bool ascending, int pageSize = 10)
        {
            return ContainerManager<TEntity>.GetResolve().GetPagedElements<S>(pageIndex, specification, orderByExpression, ascending,pageSize);
        }
        #endregion

        #region DirectSpecification
        public int GetPagedCount(DirectSpecification<TEntity> exception)
        {
            int total= ContainerManager<TEntity>.GetResolve().GetPagedCount(exception);
            return total;
        }

        public IEnumerable<TEntity> FindAll(DirectSpecification<TEntity> exception)
        {
            return ContainerManager<TEntity>.GetResolve().FindAll(exception);
        }
        public TEntity FirstOrDefault(DirectSpecification<TEntity> exception)
        {
            return ContainerManager<TEntity>.GetResolve().FirstOrDefault(exception);
        }
        public void Delete(DirectSpecification<TEntity> exception)
        {
            ContainerManager<TEntity>.GetResolve().Delete(exception);
        }
        #endregion


        #endregion
    }
}
