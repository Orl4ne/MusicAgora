using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class LibraryUserExtensions
    {
        public static LibraryUserTO ToTransferObject(this LibraryUserEF libraryUser)
        {
            //if (Instrument is null)
            //    throw new ArgumentNullException(nameof(Instrument));

            return new LibraryUserTO
            {

            };
        }

        public static LibraryUserEF ToEF(this LibraryUserTO libraryUser)
        {
            if (libraryUser is null)
                throw new ArgumentNullException(nameof(libraryUser));

            var libraryUserEF = new LibraryUserEF
            {

            };
            return libraryUserEF;
        }
    }
}
