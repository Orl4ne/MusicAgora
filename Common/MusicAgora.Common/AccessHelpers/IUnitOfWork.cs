using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.AccessHelpers
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
