using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class UsersVM
    {
        public Guid Id { get; set; }
        public long IdAccount { get; set; }
        public long IdStaff { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }

    }
}
