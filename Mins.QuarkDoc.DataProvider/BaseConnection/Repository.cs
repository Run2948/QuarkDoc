using Mins.QuarkDoc.DataInterface;
using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mins.QuarkDoc.DataProvider
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        #region EF
        /// <summary>
        /// 插入数据库记录
        /// </summary>
        /// <param name="TEntiy"></param>
        public void Insert(TEntity entiy)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    TEntity entity = context.Set<TEntity>().Add(entiy);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="item"></param>
        public void Delete(TEntity item)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    context.Set<TEntity>().Attach(item);
                    context.Set<TEntity>().Remove(item);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="speicification"></param>
        public void Delete(ISpecification<TEntity> speicification)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    IEnumerable<TEntity> entityList = context.Set<TEntity>().Where(speicification.SatisfiedBy()).ToList();
                    if (entityList.Count() > 0)
                    {
                        foreach (var item in entityList)
                        {
                            context.Set<TEntity>().Attach(item);
                            context.Set<TEntity>().Remove(item);
                        }
                        context.SaveChanges();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="item">实体对象</param>
        public void Update(TEntity item)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                if (context.Entry(item).State == EntityState.Modified)
                    context.Set<TEntity>().Attach(item);
                context.Entry(item).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="item">实体对象</param>
        /// <param name="paramField">字段</param>
        public void UpdateField(TEntity item, params string[] paramField)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    ObjectContext dbcontext = ((IObjectContextAdapter)context).ObjectContext;

                    context.Set<TEntity>().Attach(item);


                    ObjectStateEntry stateEntry = dbcontext.ObjectStateManager.GetObjectStateEntry(item);
                    foreach (string field in paramField)
                    {
                        stateEntry.SetModifiedProperty(field);
                    }

                    stateEntry.SetModified();

                    dbcontext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll()
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                return context.Set<TEntity>().ToList();
            }
        }
        /// <summary>
        /// 查找第一条或默认记录
        /// </summary>
        /// <param name="specification">过滤条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(ISpecification<TEntity> specification)
        {

            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    return context.Set<TEntity>().FirstOrDefault(specification.SatisfiedBy());
                }
                catch (DbEntityValidationException)
                {
                    throw;
                }
            }



        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="specification">过滤条件</param>
        /// <param name="includeObjects">关联对象</param>
        /// <returns></returns> 
        public IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification, bool includeDefalutObjects = false, List<string> includeObjects = null)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                DbQuery<TEntity> dbQuery = context.Set<TEntity>();

                if (includeDefalutObjects)
                {
                    if (includeObjects != null)
                        includeObjects.AddRange(GetDefaultJionObjects());
                    else
                        includeObjects = GetDefaultJionObjects();
                    includeObjects = includeObjects.Distinct().ToList();
                }
                else
                    includeObjects = new List<string>();

                foreach (var item in includeObjects)
                {
                    dbQuery = dbQuery.Include(item);
                }
                if (specification != null)
                    return dbQuery.Where(specification.SatisfiedBy()).ToList();
                else
                    return dbQuery.ToList();
            }
        }
        /// <summary>
        /// 获得行数
        /// </summary>
        /// <param name="specification">过滤条件</param>
        /// <returns></returns>
        public int GetPagedCount(ISpecification<TEntity> specification)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                try
                {
                    return (specification != null) ? context.Set<TEntity>().Count(specification.SatisfiedBy()) : context.Set<TEntity>().Count();
                }
                catch (DbEntityValidationException)
                {
                    throw;
                }
            }
        }
        public IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, List<string> includeObjects, ISpecification<TEntity> specification, Expression<Func<TEntity, S>> orderByExpression, bool ascending, int pageSize = 10)
        {
            using (DBContext<TEntity> context = new DBContext<TEntity>())
            {
                IQueryable<TEntity> entities = context.Set<TEntity>();

                ///默认添加该对象的关联属性
                if (includeObjects != null)
                    includeObjects.AddRange(GetDefaultJionObjects());
                else
                    includeObjects = GetDefaultJionObjects();

                foreach (var includeObject in includeObjects.Distinct())
                {
                    entities = entities.Include(includeObject);
                }
                if (specification != null)
                {
                    entities = entities
                        .Where(specification.SatisfiedBy());
                }
                return (ascending)
                               ?
                                   entities
                                    .OrderBy(orderByExpression)
                                    .Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize)
                                    .ToList()
                               :
                                   entities
                                    .OrderByDescending(orderByExpression)
                                    .Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize)
                                    .ToList();
            }
        }

        public IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, ISpecification<TEntity> specification, Expression<Func<TEntity, S>> orderByExpression, bool ascending, int pageSize = 10)
        {
            return GetPagedElements(pageIndex, null, specification, orderByExpression, ascending, pageSize);
        }

        /// <summary>
        /// 获取默认表关联对象
        /// </summary>
        /// <returns></returns>
        private List<string> GetDefaultJionObjects()
        {
            PropertyInfo[] propertities = typeof(TEntity).GetProperties();
            List<string> includeObjects = new List<string>();
            if (includeObjects == null)
                includeObjects = new List<string>();
            foreach (var item in propertities)
            {
                Type type = item.PropertyType;

                if (type.Namespace.Equals(typeof(TEntity).Namespace) && !type.FullName.Contains("+"))
                {
                    includeObjects.Add(item.Name);
                }
            }
            return includeObjects;
        }
        #endregion
    }
}
