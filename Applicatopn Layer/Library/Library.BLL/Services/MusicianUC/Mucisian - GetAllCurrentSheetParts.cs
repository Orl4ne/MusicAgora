using Microsoft.AspNet.Identity;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.Services.MusicianUC
{
    public partial class Musician : IMusicianUC
    {
        public List<SheetPartTO> GetAllCurrentSheetParts(int UserId)
        {
            throw new NotImplementedException();
        }

    }
}
