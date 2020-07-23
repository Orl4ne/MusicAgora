using MusicAgora.Common.Library.TransferObjects;
using System.Collections.Generic;

namespace MusicAgora.Common.Library.Interfaces.UseCases
{
    public interface IMusicianUC
    {
        List<SheetPartTO> GetAllMyCurrentSheetParts(int IdentityUserId);
        SheetPartTO GetACurrentSheetPart(int IdentityUserId, int SheetPartId);

    }
}