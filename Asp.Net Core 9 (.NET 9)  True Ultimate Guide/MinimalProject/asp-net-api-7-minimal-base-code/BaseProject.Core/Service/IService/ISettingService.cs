using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;

namespace BaseProject.Core.Service
{
    public interface ISettingService : IGenericService<Setting>
    {
        public bool AddSetting(string image);
    }
}
