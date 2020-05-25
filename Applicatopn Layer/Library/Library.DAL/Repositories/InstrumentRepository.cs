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
            return libraryContext.Instruments.Add(entity.ToEF()).Entity.ToTransferObject();
        }

        public IEnumerable<InstrumentTO> GetAll()
            => libraryContext.Instruments
                .AsNoTracking()
                .Select(x => x.ToTransferObject())
                .ToList();

        public InstrumentTO GetById(int Id)
            => libraryContext.Instruments
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTransferObject();

        public bool Remove(InstrumentTO entity)
            => Remove(entity.Id);

        public bool Remove(int Id)
        {
            var instrument = libraryContext.Instruments.FirstOrDefault(x => x.Id == Id);

            if (instrument is null)
            {
                throw new KeyNotFoundException();
            }
            try
            {
                libraryContext.Instruments.Remove(instrument);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
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
                editedEntity = entity.ToEF();
            }
            return editedEntity.ToTransferObject();
        }
    }
}
