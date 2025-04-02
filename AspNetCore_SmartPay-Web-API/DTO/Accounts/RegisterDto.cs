﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCore_SmartPay_Web_API.DTO.Accounts
{
    public class RegisterDto
    {

        [Required]
        [StringLength(15,MinimumLength=3,ErrorMessage="First name must be at least {2}, and maximu {1} characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2}, and maximu {1} characters")]
        public string LastName { get; set; }
      


        [Required]
        [RegularExpression("^[\\w\\.=-]+@[\\w\\.-]+\\.[\\w]{2,3}$", ErrorMessage = "Invaild email address")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "First name must be at least {2}, and maximu {1} characters")]
        public string Password { get; set; }
    }
}
