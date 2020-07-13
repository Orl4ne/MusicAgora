using Library.DAL.Repositories;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL
{
    public class LibraryUnitOfWork : ILibraryUnitOfWOrk
    {
        private readonly LibraryContext libraryContext;
        public LibraryUnitOfWork(LibraryContext context)
        {
            this.libraryContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ICategoryRepository categoryRepository;
        public ICategoryRepository CategoryRepository
            => categoryRepository ??= new CategoryRepository(libraryContext);

        private IInstrumentRepository instrumentRepository;
        public IInstrumentRepository InstrumentRepository
            => instrumentRepository ??= new InstrumentRepository(libraryContext);

        private ISheetRepository sheetRepository;
        public ISheetRepository SheetRepository
            => sheetRepository ??= new SheetRepository(libraryContext);

        private ISheetPartRepository sheetPartRepository;
        public ISheetPartRepository SheetPartRepository
            => sheetPartRepository ??= new SheetPartRepository(libraryContext);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    libraryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return libraryContext.SaveChanges();
        }
    }
}
