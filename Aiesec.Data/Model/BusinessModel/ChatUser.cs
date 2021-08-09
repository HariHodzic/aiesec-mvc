using Aiesec.Data.Model.IdentityModel;

namespace Aiesec.Data.Model.BusinessModel
{
    public class ChatUser
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public UserRole Role { get; set; }
    }
}