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
    public class SheetPartRepository : ISheetPartRepository
    {
        private LibraryContext libraryContext;
        public SheetPartRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        public SheetPartTO Add(SheetPartTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id != 0)
            {
                return entity;
            }
            return libraryContext.SheetParts.Add(entity.ToEF()).Entity.ToTransferObject();
        }

        public IEnumerable<SheetPartTO> GetAll()
            => libraryContext.SheetParts
                .AsNoTracking()
                .Select(x => x.ToTransferObject())
                .ToList();

        public SheetPartTO GetById(int Id)
            => libraryContext.SheetParts
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTransferObject();

        public bool Remove(SheetPartTO entity)
            => Remove(entity.Id);

        public bool Remove(int Id)
        {
            var sheetPart = libraryContext.SheetParts.FirstOrDefault(x => x.Id == Id);

            if (sheetPart is null)
            {
                throw new KeyNotFoundException();
            }
            try
            {
                libraryContext.SheetParts.Remove(sheetPart);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public SheetPartTO Update(SheetPartTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("SheetPart To Update Invalid Id");
            }
            if (!libraryContext.SheetParts.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(SheetPartTO) Can't find sheetPart to update.");
            }

            var editedEntity = libraryContext.SheetParts.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity = entity.ToEF();
            }

            return editedEntity.ToTransferObject();
        }
    }
}
