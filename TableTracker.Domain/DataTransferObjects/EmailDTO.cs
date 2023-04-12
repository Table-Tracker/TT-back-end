using System.Collections.Generic;

namespace TableTracker.Domain.DataTransferObjects
{
    public class EmailDTO
    {
        public string From { get; set; }

        public ICollection<string> To { get; set; }

        public ICollection<string> Cc { get; set; }

        public ICollection<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
