using System.ComponentModel.DataAnnotations;

namespace Aiesec.Data.ViewModel
{
    public class PasswordChange
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        [RegularExpression("\\w", ErrorMessage = "NE MOZE")]
        public string NewPassword { get; set; }
    }
}