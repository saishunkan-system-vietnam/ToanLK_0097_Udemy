using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BaseProject.Infrastructure;
using ZstdSharp.Unsafe;

namespace BaseProject.Core
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly GenericRepository<TEntity> _repository;
        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            string genericType = typeof(TEntity).Name;
            _repository = (GenericRepository<TEntity>)_unitOfWork.GetType().GetProperty($"{genericType}Repository")!.GetValue(_unitOfWork, null)!;
        }

        public IQueryable<TEntity> Get()
        {
            return _repository.Get();
        }

        public IQueryable<TEntity>? GetByWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _repository.GetByWhere(expression);
        }

        public TEntity? Add(TEntity entity)
        {
            try{
                var addedEntity = _repository.Add(entity);
                _unitOfWork.SaveChanges();
                return addedEntity;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Update(TEntity entity)
        {
            var isSuccess = _repository.Update(entity);
            _unitOfWork.SaveChanges();
            return isSuccess;
        }

        public bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            return _repository.Delete(expression);
        }

      
    }
}
