using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Domain
{
    public class AddDomainResponse
    {
        public AddDomainResponse()
        {

        }
        public AddDomainResponse(int ıd, DateTime createdTime, DateTime endTime, string domainName, string buyedDomainSite)
        {
            Id = ıd;
            CreatedTime = createdTime;
            EndTime = endTime;
            DomainName = domainName;
            BuyedDomainSite = buyedDomainSite;
        }

        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DomainName { get; set; }
        public string BuyedDomainSite { get; set; }
    }
}
