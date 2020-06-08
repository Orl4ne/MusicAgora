using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class LibraryAccessExtensions
    {
        public static LibraryAccessTO ToTransferObject(this LibraryAccessEF libraryAccess)
        {
            if (libraryAccess is null)
                throw new ArgumentNullException(nameof(libraryAccess));

            return new LibraryAccessTO
            {

            };
        }

        public static LibraryAccessEF ToEF(this LibraryAccessTO libraryAccess)
        {
            if (libraryAccess is null)
                throw new ArgumentNullException(nameof(libraryAccess));

            return new LibraryAccessEF
            {

            };
        }
    }
}
