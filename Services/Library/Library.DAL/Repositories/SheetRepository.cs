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
    public class SheetRepository : ISheetRepository
    {
        private LibraryContext libraryContext;

        public SheetRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }
        public SheetTO Add(SheetTO entity)
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
            entityEF.Category = libraryContext.Categories.First(x => x.Id == entity.Category.Id);

            var result = libraryContext.Sheets.Add(entityEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(SheetTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Sheet To Delete Invalid Id");
            }

            var sheet = libraryContext.Sheets.FirstOrDefault(x => x.Id == entity.Id);
            libraryContext.Sheets.Remove(sheet);
            libraryContext.SaveChanges();
            return true;
        }

        public IEnumerable<SheetTO> GetAll()
        {
            var list = libraryContext.Sheets
                .Include(x => x.Category)
                ?.Select(x => x.ToTransferObject())
                .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no Sheet in DB");
            }
            return list;
        }

        public SheetTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Sheet not found, invalid Id");
            }
            return libraryContext.Sheets.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }

        public SheetTO Update(SheetTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Sheet To Update Invalid Id");
            }
            if (!libraryContext.Sheets.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(SheetTO) Can't find sheet to update.");
            }
            var editedEntity = libraryContext.Sheets.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity.UpdateFromDetached(entity.ToEF());
            }
            var tracking = libraryContext.Sheets.Update(editedEntity);
            tracking.State = EntityState.Detached;
            //libraryContext.SaveChanges();

            //return editedEntity.ToTransferObject();
            return tracking.Entity.ToTransferObject();
        }
    }
}
