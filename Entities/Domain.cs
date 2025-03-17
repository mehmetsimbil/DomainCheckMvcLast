using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Domain : Entity<int>
    {
        public DateTime EndTime { get; set; }
        public string DomainName { get; set; }
        public string BuyedDomainSite { get; set; }
    }
}
