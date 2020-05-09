using Library.DAL.Entities;
using Library.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private LibraryContext libraryContext;

        public CategoryRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }
        public CategoryTO Add(CategoryTO entity)
        {
            if(entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id!=0)
            {
                return entity;
            }
            return libraryContext.Categories.Add(entity.ToEF()).Entity.ToTransferObject();
        }

        public IEnumerable<CategoryTO> GetAll()
            => libraryContext.Categories
                .AsNoTracking()
                .Select(x => x.ToTransferObject())
                .ToList();

        public CategoryTO GetById(int Id)
            => libraryContext.Categories
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTransferObject();

        public bool Remove(CategoryTO entity)
            => Remove(entity.Id);

        public bool Remove(int Id)
        {
            var category = libraryContext.Categories.FirstOrDefault(x => x.Id == Id);

            if (category is null)
            {
                throw new KeyNotFoundException();
            }
            try
            {
                libraryContext.Categories.Remove(category);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public CategoryTO Update(CategoryTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Category To Update Invalid Id");
            }
            if (!libraryContext.Categories.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(CategoryTO) Can't find category to update.");
            }
            
            var editedEntity = libraryContext.Categories.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity = entity.ToEF();
            }
            
            return editedEntity.ToTransferObject();

        }
    }
}
