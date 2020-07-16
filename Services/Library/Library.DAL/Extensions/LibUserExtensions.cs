using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class LibUserExtensions
    {
        public static LibUserTO ToTransferObject(this LibUserEF LibUser)
        {
            if (LibUser is null)
                throw new ArgumentNullException(nameof(LibUser));

            return new LibUserTO
            {
                Id = LibUser.Id,
                IdentityUserId = LibUser.IdentityUserId,
                InstrumentIds = LibUser.UserInstruments?.Select(x => x.InstrumentId).ToList(),
            };
        }

        public static LibUserEF ToEF(this LibUserTO LibUser)
        {
            if (LibUser is null)
                throw new ArgumentNullException(nameof(LibUser));

            var libUserEF = new LibUserEF
            {
                Id = LibUser.Id,
                IdentityUserId = LibUser.IdentityUserId,
            };
            
            libUserEF.UserInstruments = LibUser.InstrumentIds?.Select(x => new UserInstruEF
            {
                LibUserId = libUserEF.Id,
                InstrumentId = x,
            }).ToList();

            return libUserEF;
        }

        public static LibUserEF ToTrackedEF(this LibUserTO LibUser, LibUserEF LibUserToModify)
        {
            if (LibUserToModify is null)
                throw new ArgumentNullException(nameof(LibUserToModify));
            if (LibUser is null)
                throw new ArgumentNullException(nameof(LibUser));

            LibUserToModify.Id = LibUser.Id;
            LibUserToModify.IdentityUserId = LibUser.IdentityUserId;

            return LibUserToModify;
        }
    }
}
