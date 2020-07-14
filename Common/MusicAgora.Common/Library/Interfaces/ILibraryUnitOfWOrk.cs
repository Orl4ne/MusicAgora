using MusicAgora.Common.AccessHelpers;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.Interfaces
{
    public interface ILibraryUnitOfWOrk : IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IInstrumentRepository InstrumentRepository { get; }
        ISheetRepository SheetRepository { get; }
        ISheetPartRepository SheetPartRepository { get; }
        ILibUserRepository LibUserRepository { get; }
    }
}
