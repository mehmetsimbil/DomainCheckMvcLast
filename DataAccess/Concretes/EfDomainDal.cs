using DataAccess.Abstracts;
using DataAccess.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfDomainDal : EfRepositoryBase<Domain, ProjectContext>, IDomainDal
    {
        public EfDomainDal(ProjectContext context) : base(context)
        {
        }
    }
}
