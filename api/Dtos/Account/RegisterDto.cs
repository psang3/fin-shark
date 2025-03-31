using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}