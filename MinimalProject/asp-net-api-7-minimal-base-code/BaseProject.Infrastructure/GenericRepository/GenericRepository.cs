using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly DBMasterContext dbMasterContext;
        public readonly DBSlaveContext dbSlaveContext;

        private readonly DbSet<TEntity>  _dbMasterSet;
        private readonly DbSet<TEntity>  _dbSlaveSet;

        public GenericRepository(DBMasterContext dbMasterContext, DBSlaveContext dbSlaveContext)
        {
            this.dbMasterContext = dbMasterContext;
            _dbMasterSet = this.dbMasterContext.Set<TEntity>(); 
            this.dbSlaveContext = dbSlaveContext;
            _dbSlaveSet = this.dbSlaveContext.Set<TEntity>();
        }

        /// <summary>
        /// get all <TEntity>
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Get()
        {
            return _dbSlaveSet.Where(x=>true);
        }

        public TEntity Add(TEntity entity)
        {
            _dbMasterSet.Add(entity);
            return entity;
        }

        public bool Update(TEntity entity)
        {
            _dbMasterSet.Update(entity);
            return true;
        }

        public bool Delete(Expression<Func<TEntity,bool>> expression)
        {
            var query = _dbMasterSet.Where(expression);
            query.ExecuteDelete();
            return true;
        }

        public IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSlaveSet.Where(expression);
        }
    }
}
