using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Domain.Constant
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }
}
