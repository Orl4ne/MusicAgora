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
    public class LibUserRepository : ILibUserRepository
    {
        private LibraryContext libraryContext;
        public LibUserRepository(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        public LibUserTO Add(LibUserTO entity)
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
            var result = libraryContext.LibraryUsers.Add(entityEF);
            libraryContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(LibUserTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("LibUser To Delete Invalid Id");
            }

            var libUser = libraryContext.LibraryUsers.FirstOrDefault(x => x.Id == entity.Id);
            libraryContext.LibraryUsers.Remove(libUser);
            libraryContext.SaveChanges();
            return true;
        }

        public IEnumerable<LibUserTO> GetAll()
        {
            var list = libraryContext.LibraryUsers.Include(x => x.UserInstruments)
               .AsEnumerable()
               ?.Select(x => x.ToTransferObject())
               .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no LibUser in DB");
            }
            return list;
        }

        public LibUserTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("LibUser not found, invalid Id");
            }
            return libraryContext.LibraryUsers.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }
        public LibUserTO GetByIdentityUserId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("LibUser not found, invalid Id");
            }
            return libraryContext.LibraryUsers.FirstOrDefault(x => x.IdentityUserId == id).ToTransferObject();
        }

        public LibUserTO Update(LibUserTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("LibUser To Update Invalid Id");
            }
            if (!libraryContext.LibraryUsers.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(LibUser) Can't find LibUser to update.");
            }

            var editedEntity = libraryContext.LibraryUsers.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                editedEntity.UpdateFromDetached(entity.ToEF());
            }
            var tracking = libraryContext.LibraryUsers.Update(editedEntity);
            tracking.State = EntityState.Detached;
            libraryContext.SaveChanges();

            //return editedEntity.ToTransferObject();
            return tracking.Entity.ToTransferObject();

            //return editedEntity.ToTransferObject();
        }
    }
}
