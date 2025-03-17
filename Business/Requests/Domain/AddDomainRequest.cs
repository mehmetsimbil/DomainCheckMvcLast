using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Requests.Domain
{
    public class AddDomainRequest
    {
        public AddDomainRequest()
        {

        }
        public AddDomainRequest(DateTime endTime, string domainName, string buyedDomainSite)
        {
            EndTime = endTime;
            DomainName = domainName;
            BuyedDomainSite = buyedDomainSite;
        }

        public DateTime EndTime { get; set; }
        public string DomainName { get; set; }
        public string BuyedDomainSite { get; set; }
    }
}
