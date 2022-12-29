using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class RolesVM
    {
        public RolesVM()
        {
            Users = new List<string>();
        }
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<string> Users { get; set; }
    }
}
