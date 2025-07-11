using CustomerManagement.Domain.Common;

namespace CustomerManagement.Domain
{
    public class Audit : BaseEntity
    {
        public long AuditId { get; set; }

        public string RowIds { get; set; }

        public string EntityName { get; set; }

        public string NewRowValues { get; set; }

        public string OldRowValues { get; set; }

        public string OperationType { get; set; }
    }
}
