using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.Interfaces.IRepositories
{
    public interface ILibraryUnitOfWork : IDisposable
    {
        void Save();
    }
}
