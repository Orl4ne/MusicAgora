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
                Instruments = LibUser.UserInstruments?.Select(x => x.Instrument.ToTransferObject()).ToList(),
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

            libUserEF.UserInstruments = LibUser.Instruments?.Select(x => new UserInstruEF
            {
                LibUser = libUserEF,
                LibUserId = libUserEF.Id,
                Instrument = x.ToEF(),
                InstruId = x.Id,
            }).ToList();

            return libUserEF;
        }

        public static LibUserEF ToTrackedEF(this LibUserTO LibUser, LibUserEF LibUserTOModify)
        {
            if (LibUserTOModify is null)
                throw new ArgumentNullException(nameof(LibUserTOModify));
            if (LibUser is null)
                throw new ArgumentNullException(nameof(LibUser));

            //LibUserTOModify.Id = LibUser.Id;
            //LibUserTOModify.Name = LibUser.Name;

            return LibUserTOModify;
        }
    }
}
