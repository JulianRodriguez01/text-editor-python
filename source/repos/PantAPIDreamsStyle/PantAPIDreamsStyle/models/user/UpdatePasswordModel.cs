using System.ComponentModel.DataAnnotations;

namespace PantAPIDreamsStyle.models.user
{
    public class UpdatePasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
