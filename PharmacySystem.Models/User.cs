using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class User
    {
        public User()
        {
            ExportInvoices = new HashSet<ExportInvoice>();
            ImportInvoices = new HashSet<ImportInvoice>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserTokens = new HashSet<UserToken>();
            Roles = new HashSet<Role>();
        }

        public long Id { get; set; }
        public long IdStaff { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual staff IdStaffNavigation { get; set; } = null!;
        public virtual ICollection<ExportInvoice> ExportInvoices { get; set; }
        public virtual ICollection<ImportInvoice> ImportInvoices { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
