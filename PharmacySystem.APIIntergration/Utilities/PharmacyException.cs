using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration.Utilities
{
    public class PharmacyException : Exception
    {
        public PharmacyException()
        {
        }

        public PharmacyException(string message)
            : base(message)
        {
        }

        public PharmacyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
