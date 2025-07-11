using CustomerManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Domain
{
    public class CommunicationTemplate : BaseEntity
    {
        public int CommunicationTemplateId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}