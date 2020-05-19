using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.DTOs
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(10,MinimumLength=4,ErrorMessage="Error from DTO")]
        public string Password { get; set; }
    }
}