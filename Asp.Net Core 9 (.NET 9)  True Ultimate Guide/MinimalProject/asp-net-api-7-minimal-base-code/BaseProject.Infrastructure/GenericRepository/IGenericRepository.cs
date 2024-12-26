using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public interface IGenericRepository<TEntity>
    {
        public IQueryable<TEntity> Get();
        public TEntity Add(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(Expression<Func<TEntity, bool>> expression);
        public IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> expression);
    }
}
