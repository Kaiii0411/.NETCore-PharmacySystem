using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class staff
    {
        public staff()
        {
            Accounts = new HashSet<Account>();
        }

        public long IdStaff { get; set; }
        public string? StaffName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public long? IdStore { get; set; }

        public virtual Store? IdStoreNavigation { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
