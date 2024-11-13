using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FOABE.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlternativeNumber { get; set; } = string.Empty;
        //public string Role { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public UserType UserType { get; set; } = UserType.None;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string PinCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public ICollection<Order>? Orders { get; set; }  
        public ICollection<Restaurant>? RestaurantsOwned { get; set; }  
        public virtual ICollection<Review>? Reviews { get; set; }
        public Cart? Cart { get; set; }
       
    }

}
