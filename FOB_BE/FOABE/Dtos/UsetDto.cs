using FOABE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace FOABE.Dtos
{

    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlternativeNumber { get; set; } = string.Empty;
        public UserType UserType { get; set; } = UserType.None;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<RestaurantDto>? RestaurantsOwned { get; set; }
    }

}


