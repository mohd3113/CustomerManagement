using CustomerManagement.Domain;
using CustomerManagement.Domain.Common;
using CustomerManagement.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerManagement.Infrastructure.Data
{
    public class AuditableDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AuditableDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Audit> Audits { get; set; }

        public virtual async Task<int> SaveChangeAsync()
        {
            var result = await base.SaveChangesAsync();
            return result;
        }

        public virtual async Task<int> SaveChangesAsync(string username)
        {
            var auditEntries = OnBeforeSaveChanges(username);
            var result = await base.SaveChangesAsync();
            await OnAfterSaveChanges(auditEntries);
            return result;
        }
        public List<AuditEntry> OnBeforeSaveChanges(string username)
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();
            var modifiedChanges = ChangeTracker.Entries().Where(p => p.Entity is not Audit && p.Entity is not IdentityUser && p.State != EntityState.Detached && p.State != EntityState.Unchanged);
            foreach (var entry in modifiedChanges)
            {
                var auditEntry = new AuditEntry();
                auditEntry.UpdatedBy = username;
                auditEntry.EntityName = entry.Metadata.ClrType.Name;
                auditEntry.TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList();

                foreach (var property in entry.Properties)
                {
                    ((BaseEntity)entry.Entity).LastModifiedDate = DateTime.Now;
                    ((BaseEntity)entry.Entity).LastModifiedBy = username;

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.RowIds[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {

                        case EntityState.Added:
                            auditEntry.OperationType = AuditType.Create;
                            auditEntry.NewRowValues[propertyName] = property.CurrentValue;
                            ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
                            ((BaseEntity)entry.Entity).CreatedBy = username;
                            break;
                        case EntityState.Deleted:
                            auditEntry.OperationType = AuditType.Delete;
                            auditEntry.OldRowValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            auditEntry.OperationType = AuditType.Update;
                            auditEntry.OldRowValues[propertyName] = property.OriginalValue;
                            auditEntry.NewRowValues[propertyName] = property.CurrentValue;
                            break;
                    }

                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        public async Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return;
            }

            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties.Where(p => p.Metadata.IsPrimaryKey()))
                {
                    entry.RowIds[prop.Metadata.Name] = prop.CurrentValue;
                }

                await Audits.AddAsync(entry.ToAudit());
            }

            await SaveChangeAsync();
        }
    }
}