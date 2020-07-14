using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Repositories
{
    public class LibUserRepository : ILibUserRepository
    {
        private LibraryContext libraryContext;
        public LibUserRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        public LibUserTO Add(LibUserTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(LibUserTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibUserTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public LibUserTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public LibUserTO Update(LibUserTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
