using System;
using System.Linq.Expressions;
namespace Mins.QuarkDoc.Framework
{
    public sealed class DirectSpecification<TEntity>
      : Specification<TEntity>
      where TEntity : class
    {
        #region Members

        Expression<Func<TEntity, bool>> _MatchingCriteria;

        #endregion

        #region Constructor
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");
            _MatchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }
        #endregion



    }
}
