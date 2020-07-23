using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.BLL.UseCases
{
    public class LibrarianUC : ChiefUC, ILibrarianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        public LibrarianUC(ILibraryUnitOfWork iLibraryUnitOfWork) : base (iLibraryUnitOfWork)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
        }
        #endregion

        public SheetTO CreateANewSheet(SheetTO Sheet)
        {
            throw new NotImplementedException();
        }

        public SheetPartTO UploadSheetPartInSheet(SheetPartTO SheetPart)
        {
            throw new NotImplementedException();
        }
    }
}
