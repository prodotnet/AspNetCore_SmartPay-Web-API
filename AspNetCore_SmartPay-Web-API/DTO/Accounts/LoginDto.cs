using System.ComponentModel.DataAnnotations;

namespace AspNetCore_SmartPay_Web_API.DTO.Accounts
{
    public class LoginDto
    {
        [Required]
        [RegularExpression("^[\\w\\.=-]+@[\\w\\.-]+\\.[\\w]{2,3}$", ErrorMessage="Invaild email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
