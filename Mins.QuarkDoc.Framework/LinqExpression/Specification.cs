using System;
using System.Linq.Expressions;

namespace Mins.QuarkDoc.Framework
{
    public abstract class Specification<TEntity>
        : ISpecification<TEntity>
        where TEntity : class
    {
        #region ISpecification<TEntity> Members
        public abstract Expression<Func<TEntity, bool>> SatisfiedBy();

        #endregion
    }
}
