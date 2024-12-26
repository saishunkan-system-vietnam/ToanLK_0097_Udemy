using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.Service
{
    public interface INewsService : IGenericService<News>
    {
        public bool DeleteRange(List<Guid> listOfId);
        public bool AddNews(NewsInsertDto news);
        public bool UpdateNews(NewsInsertDto news);

    }
}
