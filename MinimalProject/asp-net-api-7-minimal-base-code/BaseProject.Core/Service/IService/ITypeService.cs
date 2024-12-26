using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;

namespace BaseProject.Core.Service
{
    public interface ITypeService : IGenericService<Shared.Modal.Entity.Type>
    {
        public bool AddType(TypeInsertDto type);
        public bool UpdateFlagShow(TypeUpdateFlagShowDto updateDto);
        public List<Shared.Modal.Entity.Type> GetByShowLandingPage();
        public bool Update(TypeInsertDto type);
    }
}
