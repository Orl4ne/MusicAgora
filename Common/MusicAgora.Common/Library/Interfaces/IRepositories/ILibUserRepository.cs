using MusicAgora.Common.AccessHelpers;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAgora.Common.Library.Interfaces.IRepositories
{
    public interface ILibUserRepository: IRepository<LibUserTO, int>
    {
        LibUserTO GetByIdentityUserId(int id);
    }
}
