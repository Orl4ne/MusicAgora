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
            var entityEF = entity.ToEF();
            entityEF.Instrument = libraryContext.Instruments.First(x => x.Id == entity.Instrument.Id);
            entityEF.Sheet = libraryContext.Sheets.First(x => x.Id == entity.Sheet.Id);

            var result = libraryContext.SheetParts.Add(entityEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(SheetPartTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("SheetPart To Delete Invalid Id");
            }

            var sheetPart = libraryContext.SheetParts.FirstOrDefault(x => x.Id == entity.Id);
            libraryContext.SheetParts.Remove(sheetPart);
            libraryContext.SaveChanges();
            return true;
        }

        public IEnumerable<SheetPartTO> GetAll()
        {
            var list = libraryContext.SheetParts
                .Include(x => x.Sheet)
                .Include(x => x.Instrument)
                ?.Select(x => x.ToTransferObject())
                .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no SheetPart in DB");
            }
            return list;
        }

        public SheetPartTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("SheetPart not found, invalid Id");
            }
            return libraryContext.SheetParts.FirstOrDefault(x => x.Id == id).ToTransferObject();
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
                entity.ToTrackedEF(editedEntity);
            }
            libraryContext.SaveChanges();

            return editedEntity.ToTransferObject();
        }
    }
}
