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
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id != 0)
                return entity;

            var sheetEF = entity.ToEF();

            var result = libraryContext.Sheets.Add(sheetEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public IEnumerable<SheetTO> GetAll()
            => libraryContext.Sheets
                .AsNoTracking()
                .Select(x => x.ToTransferObject())
                .ToList();

        public SheetTO GetById(int Id)
            => libraryContext.Sheets
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTransferObject();

        public bool Remove(SheetTO entity)
            => Remove(entity.Id);

        public bool Remove(int Id)
        {
            var sheet = libraryContext.Sheets.FirstOrDefault(x => x.Id == Id);

            if (sheet is null)
            {
                throw new KeyNotFoundException();
            }
            try
            {
                libraryContext.Sheets.Remove(sheet);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
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
            var editedEntity = libraryContext.Sheets.AsNoTracking().FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity = entity.ToEF();
            }
            return editedEntity.ToTransferObject();
        }
    }
}
