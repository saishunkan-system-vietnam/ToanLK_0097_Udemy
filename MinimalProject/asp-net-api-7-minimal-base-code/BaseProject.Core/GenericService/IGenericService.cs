using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core
{
    public interface IGenericService<TEntity>
    {
        public IQueryable<TEntity> Get();
        public IQueryable<TEntity>? GetByWhere(Expression<Func<TEntity, bool>> expression);
        public TEntity? Add(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(Expression<Func<TEntity, bool>> expression);
    }
}
