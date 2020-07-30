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
    public class InstrumentRepository : IInstrumentRepository
    {
        private LibraryContext libraryContext;
        public InstrumentRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }
        public InstrumentTO Add(InstrumentTO entity)
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
            var result = libraryContext.Instruments.Add(entityEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(InstrumentTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Instrument To Delete Invalid Id");
            }

            var instrument = libraryContext.Instruments.FirstOrDefault(x => x.Id == entity.Id);
            libraryContext.Instruments.Remove(instrument);
            libraryContext.SaveChanges();
            return true;
        }

        public IEnumerable<InstrumentTO> GetAll()
        {
            var list = libraryContext.Instruments.Include(x=>x.UserInstruments)
                .AsEnumerable()
                ?.Select(x => x.ToTransferObject())
                .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no Instrument in DB");
            }
            return list;
        }

        public InstrumentTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Instrument not found, invalid Id");
            }
            return libraryContext.Instruments.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }


        public InstrumentTO Update(InstrumentTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Instrument To Update Invalid Id");
            }
            if (!libraryContext.Instruments.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(InstrumentTO) Can't find instrument to update.");
            }

            var editedEntity = libraryContext.Instruments.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity.UpdateFromDetached(entity.ToEF());
            }
            var tracking = libraryContext.Instruments.Update(editedEntity);
            tracking.State = EntityState.Detached;
            libraryContext.SaveChanges();

            //return editedEntity.ToTransferObject();
            return tracking.Entity.ToTransferObject();

            //return editedEntity.ToTransferObject();
        }
    }
}
