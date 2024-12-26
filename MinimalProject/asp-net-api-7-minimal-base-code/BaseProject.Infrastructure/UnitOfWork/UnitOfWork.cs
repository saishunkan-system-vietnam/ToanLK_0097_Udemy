using BaseProject.Infrastructure;
using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBMasterContext _dbMasterContext;
        private readonly DBSlaveContext _dbSlaveContext;

        public UnitOfWork(DBMasterContext dbMasterContext, DBSlaveContext dbSlaveContext)
        {
            _dbMasterContext = dbMasterContext;
            _dbSlaveContext = dbSlaveContext;
        }

        public void SaveChanges()
        {
            _dbMasterContext.SaveChanges();
            
        }

        private readonly ProductRepository _productRepository;
        private readonly AccountRepository _accountRepository;
        private readonly TypeRepository _typeRepository;
        private readonly SettingRepository _settingRepository;
        private readonly CartRepository _cartRepository;
        private readonly NewsRepository _newsRepository;


        public DBMasterContext DBMasterContext => _dbMasterContext;
        public DBSlaveContext DBSlaveContext => _dbSlaveContext;


        public ProductRepository ProductRepository => _productRepository ?? new ProductRepository(_dbMasterContext, _dbSlaveContext);
        public AccountRepository AccountRepository => _accountRepository ?? new AccountRepository(_dbMasterContext, _dbSlaveContext);
        public TypeRepository TypeRepository => _typeRepository ?? new TypeRepository(_dbMasterContext, _dbSlaveContext);
        public SettingRepository SettingRepository=> _settingRepository ?? new SettingRepository(_dbMasterContext, _dbSlaveContext);
        public CartRepository CartRepository => _cartRepository ?? new CartRepository(_dbMasterContext, _dbSlaveContext);
        public NewsRepository NewsRepository => _newsRepository ?? new NewsRepository(_dbMasterContext, _dbSlaveContext);
    }
}
