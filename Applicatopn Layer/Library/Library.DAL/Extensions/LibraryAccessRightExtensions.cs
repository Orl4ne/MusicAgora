using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class LibraryAccessRightExtensions
    {
        public static LibraryAccessRightTO ToTransferObject(this LibraryAccessRightEF libraryAccess)
        {
            if (libraryAccess is null)
                throw new ArgumentNullException(nameof(libraryAccess));

            return new LibraryAccessRightTO
            {

            };
        }

        public static LibraryAccessRightEF ToEF(this LibraryAccessRightTO libraryAccess)
        {
            if (libraryAccess is null)
                throw new ArgumentNullException(nameof(libraryAccess));

            return new LibraryAccessRightEF
            {

            };
        }
    }
}
