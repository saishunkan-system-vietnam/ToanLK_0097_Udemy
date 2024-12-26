using BaseProject.Infrastructure;
using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public interface IUnitOfWork
    {
        ProductRepository ProductRepository { get; }
        AccountRepository AccountRepository { get; }
        TypeRepository TypeRepository { get; }
        SettingRepository SettingRepository { get; }
        CartRepository CartRepository { get; }
        NewsRepository NewsRepository { get; }
        
        DBMasterContext DBMasterContext { get; }
        DBSlaveContext DBSlaveContext { get; }

        void SaveChanges();
    }
}
