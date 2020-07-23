using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.UseCases
{
    public class ChiefUC : MusicianUC, IChiefUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        public ChiefUC(ILibraryUnitOfWork iLibraryUnitOfWork) : base (iLibraryUnitOfWork)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
        }

        public List<SheetTO> GetAllSheets()
        {
            throw new NotImplementedException();
        }

        public SheetTO SetAsCurrentSheet(int IdentityUserId, int SheetId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
