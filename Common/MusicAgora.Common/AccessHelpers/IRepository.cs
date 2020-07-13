using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.AccessHelpers
{
    public interface IRepository<TType, TIdType>
        where TType : class
    {
        bool Delete(TType entity);
        IEnumerable<TType> GetAll();
        TType GetById(TIdType id);
        TType Add(TType entity);
        TType Update(TType entity);
    }
}
