using BaseProject.Shared.Modal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure
{
    public interface INewsRepository : IGenericRepository<Shared.Modal.Entity.News>
    {
    }
}
