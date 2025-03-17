using Business.Requests.Domain;
using Business.Responses.Domain;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IDomainService
    {
        public AddDomainResponse Add(AddDomainRequest request);
        public GetDomainListResponse GetList(GetDomainListRequest request);
        public UpdateDomainResponse Update(UpdateDomainRequest request);
        public DeleteDomainResponse Delete(DeleteDomainRequest request);
        public GetDomainByIdResponse GetById(GetDomainByIdRequest request);
        public Task<GetDomainListResponse> GetListMin90Days(GetDomainListRequest request);
        Task ImportDomainsAsync(string filePath);
        //Task<string> ExportDomainsToExcelAsync();
        public IList<Domain> GetListToExcel();
        public IList<Domain> GetListToExcelLast15Days();
    }
}
