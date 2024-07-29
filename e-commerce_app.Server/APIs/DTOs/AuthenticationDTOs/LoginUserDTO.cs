using System.ComponentModel.DataAnnotations;

namespace e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
