using CMD.Contract.Models;
using System.Collections.Generic;

namespace CMD.DAL.DALs
{
    public abstract class BaseHandler<TBo> where TBo : BaseBo
    {
        public abstract void SaveRecord(TBo bo);

        public abstract bool DuplicateQuery(string name);

        public abstract List<TBo> LoadBos();

        public abstract bool DeleteRecord(long id);
    }
}
