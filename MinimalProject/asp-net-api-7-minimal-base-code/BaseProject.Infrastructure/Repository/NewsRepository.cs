﻿using BaseProject.Infrastructure;
using BaseProject.Shared.Modal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public class NewsRepository : GenericRepository<Shared.Modal.Entity.News>, INewsRepository
    {
        public NewsRepository(DBMasterContext dbmasterContext, DBSlaveContext dbSlaveContext) : base(dbmasterContext, dbSlaveContext)
        {

        }
    }
}
