using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Repositories
{
    public class LibraryUserRespository : ILibraryUserRepository
    {
        private LibraryContext libraryContext;
        public LibraryUserRespository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        public LibraryUserTO Add(LibraryUserTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibraryUserTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public LibraryUserTO GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(LibraryUserTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public LibraryUserTO Update(LibraryUserTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
