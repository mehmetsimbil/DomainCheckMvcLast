using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Domain
{
    public class GetDomainByIdResponse
    {
        public int Id { get; set; }
        public DateTime EndTime { get; set; }
        public string DomainName { get; set; }
        public string BuyedDomainSite { get; set; }
    }
}
