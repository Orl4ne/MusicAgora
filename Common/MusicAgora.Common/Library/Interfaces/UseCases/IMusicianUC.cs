﻿using MusicAgora.Common.Library.TransferObjects;
using System.Collections.Generic;
using System.IO;

namespace MusicAgora.Common.Library.Interfaces.UseCases
{
    public interface IMusicianUC
    {
        List<SheetPartTO> GetAllMyCurrentSheetParts(int IdentityUserId);
        SheetPartTO SeeASheetPartDetails(int SheetPartId);
        string DowloadSheetPart(int SheetPartId);
    }
}