﻿using Microsoft.Extensions.Configuration;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.UseCases
{
    public class ChiefUC : IChiefUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public ChiefUC(ILibraryUnitOfWork iLibraryUnitOfWork, IConfiguration config) 
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
            this.config = config;
        }
        #endregion

        public List<SheetPartTO> GetAllSheetPartsBySheet(int SheetId)
        {
            throw new NotImplementedException();
        }
        
        public List<SheetTO> GetAllSheets()
        {
            throw new NotImplementedException();
        }

        public SheetTO SetAsCurrentSheet(int IdentityUserId, int SheetId)
        {
            throw new NotImplementedException();
        }
    }
}
