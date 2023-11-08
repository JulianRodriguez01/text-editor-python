using System.ComponentModel.DataAnnotations;

namespace PantAPIDreamsStyle.models.user
{
    public class RecoveryEmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
