using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.Interfaces.UseCases
{
    public interface IChiefUC
    {
        List<SheetTO> GetAllSheets();
        SheetTO SetAsCurrentSheet(int IdentityUserId, int SheetId);
        List<SheetPartTO> GetAllSheetPartsBySheet(int SheetId);
    }
}
