using BaseProject.Shared.Modal.Enum;
using System.ComponentModel.DataAnnotations;


namespace BaseProject.Shared.Entity
{
    
    public class Account : BaseEntity
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; } = AccountRole.Customer;
        public string? Address { get; set; } = "";
        public string? Avatar { get; set; } = "";
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }   
        public bool? EmailValidated { get; set; }
        //public string? ProvinceId { get; set; }
        //public string? DistrictId { get; set; }
        //public string? WardId { get; set; }
    }
}
