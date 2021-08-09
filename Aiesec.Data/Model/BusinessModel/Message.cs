using System;

namespace Aiesec.Data.Model.BusinessModel
{
    public class Message : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}