using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DbContext libraryContext;

        public CategoryRepository(DbContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }
        public CategoryTO Add(CategoryTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryTO GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(CategoryTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public CategoryTO Update(CategoryTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
