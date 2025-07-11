using CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace CustomerManagement.Infrastructure.Helpers
{
    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }

    public class AuditEntry
    {
        public long AuditId { get; set; }
        public string EntityName { get; set; }
        public Dictionary<string, object> RowIds { get; set; }
        public Dictionary<string, object> OldRowValues { get; set; }
        public Dictionary<string, object> NewRowValues { get; set; }
        public string UpdatedBy { get; set; }
        public AuditType OperationType { get; set; }
        public List<PropertyEntry> TempProperties { get; set; }
        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.AuditId = AuditId;
            audit.EntityName = EntityName;
            audit.OperationType = OperationType.ToString();
            audit.RowIds = JsonConvert.SerializeObject(RowIds);
            audit.NewRowValues = NewRowValues.Count == 0 ? null : JsonConvert.SerializeObject(NewRowValues);
            audit.OldRowValues = OldRowValues.Count == 0 ? null : JsonConvert.SerializeObject(OldRowValues);
            audit.LastModifiedBy = UpdatedBy;
            audit.CreatedBy = UpdatedBy;
            audit.DateCreated = DateTime.Now;
            audit.LastModifiedDate = DateTime.Now;
            return audit;
        }

        public AuditEntry()
        {
            RowIds = new Dictionary<string, object>();
            OldRowValues = new Dictionary<string, object>();
            NewRowValues = new Dictionary<string, object>();
        }

    }
}