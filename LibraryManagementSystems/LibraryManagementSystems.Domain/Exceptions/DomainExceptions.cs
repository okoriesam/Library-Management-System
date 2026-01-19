using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Domain.Exceptions
{
    public class DomainExceptions : Exception
    {
        public DomainExceptions()
        {

        }

        public DomainExceptions(string message) : base(message)
        {

        }

        public DomainExceptions(string message, Exception exception) : base(message, exception) { }
    }
}
