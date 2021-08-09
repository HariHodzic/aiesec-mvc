using System.Collections.Generic;
using Aiesec.Data.Model.IdentityModel;

namespace Aiesec.Data.Model.BusinessModel
{
    public class Chat : BaseEntity<int>
    {
        public Chat()
        {
            Messages = new List<Message>();
            Users = new List<ChatUser>();
        }

        public string Name { get; set; }
        public ChatType Type { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> Users { get; set; }
    }
}