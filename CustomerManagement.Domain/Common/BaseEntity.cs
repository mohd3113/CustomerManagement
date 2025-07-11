using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Domain.Common
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }

        public DateTime LastModifiedDate { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [MaxLength(100)]
        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
