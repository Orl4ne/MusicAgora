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
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id != 0)
            {
                return entity;
            }

            var entityEF = entity.ToEF();
            var result = libraryContext.Categories.Add(entityEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public IEnumerable<CategoryTO> GetAll()
        {
            var list = libraryContext.Categories.AsEnumerable()
                 ?.Select(x => x.ToTransferObject())
                 .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no Category in DB");
            }
            return list;
        }

        public CategoryTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Category not found, invalid Id");
            }
            return libraryContext.Categories.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }

        public bool Delete(CategoryTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Category To Delete Invalid Id");
            }

            var category = libraryContext.Categories.FirstOrDefault(x => x.Id == entity.Id);
            libraryContext.Categories.Remove(category);
            libraryContext.SaveChanges();
            return true;
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
                editedEntity.UpdateFromDetached(entity.ToEF());
            }
            var tracking = libraryContext.Categories.Update(editedEntity);
            tracking.State = EntityState.Detached;
            //libraryContext.SaveChanges();

            //return editedEntity.ToTransferObject();
            return tracking.Entity.ToTransferObject();

            //return editedEntity.ToTransferObject();
        }
    }
}
