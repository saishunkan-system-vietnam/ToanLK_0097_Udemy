using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.LinkTable;

namespace BaseProject.Shared.Modal.Entity
{
    // keycap, switch, connect type,
    public class Technology : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public virtual ICollection<ProductTechnology> ProductTechnologies { get; set; }
    }
}
