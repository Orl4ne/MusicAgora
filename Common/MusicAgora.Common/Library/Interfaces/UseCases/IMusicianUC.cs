//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using MusicAgora.Common.Library.TransferObjects;
using System.Collections.Generic;

namespace MusicAgora.Common.Library.Interfaces.UseCases
{
    public interface IMusicianUC
    {
        //User CreateUserAccount(IUser user);
        List<SheetPartTO> GetAllCurrentSheetParts(int UserId);
        SheetPartTO GetACurrentSheetPart(int UserId);
        bool UpdateMyAccount(int UserId);

        
    }
}