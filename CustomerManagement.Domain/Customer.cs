using CustomerManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Domain
{
    public class Customer : BaseEntity
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}