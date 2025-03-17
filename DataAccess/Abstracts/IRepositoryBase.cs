using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IRepositoryBase<TEntity>
    {
        public IList<TEntity> GetList(Func<TEntity, bool>? predicate = null);
        public TEntity? Get(Func<TEntity, bool> predicate);
        public TEntity Add(TEntity entity);
        public TEntity Delete(TEntity entity, bool IsDeleted = true);
        public TEntity Update(TEntity entity);
    }
}
