using Library.DAL;
using Library.DAL.Entities;
using Library.DAL.Extensions;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library.BLL.Services.MusicianUC
{
    public partial class Musician : IMusicianUC
    {
        public List<SheetPartTO> GetAllCurrentSheetParts(int userId)
        {
            var Instruments = context.UserInstruments.Where(x => x.UserID == userId).Select(x => x.Instrument);
            // Selecting the currentSheetparts for each instrument of the user
            return context.SheetParts
                    .Where(s => Instruments.Contains(s.Instrument) && s.Sheet.IsCurrent == true)
                    .Select(s => s.ToTransferObject())
                    .ToList();
        }

    }
}
