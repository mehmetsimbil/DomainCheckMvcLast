using Business.Dto_s.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Domain
{
    public class GetDomainListResponse
    {
        public ICollection<DomainListItemDto> Items { get; set; }
        public GetDomainListResponse()
        {
            Items = Array.Empty<DomainListItemDto>();
        }

        public GetDomainListResponse(ICollection<DomainListItemDto> items)
        {
            Items = items;
        }

    }
}
