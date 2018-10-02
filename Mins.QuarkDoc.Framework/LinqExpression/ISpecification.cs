using System;
using System.Linq.Expressions;

namespace Mins.QuarkDoc.Framework
{
    public interface ISpecification<TEntity>
             where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}
