using Microsoft.AspNetCore.Http;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAgora.WebUx.MVC.Models
{
    public class LibraryVM
    {
        public List<CategoryTO> AllCategories { get; set; }
        public string SelectedCategory { get; set; }

        public SheetTO Sheet { get; set; }
        public List<SheetTO> AllSheets { get; set; }
        public string SelectedSheet { get; set; }

        public SheetPartTO SheetPart { get; set; }
        public List<SheetPartTO> SheetPartsFromSheet { get; set; }
        public IFormFile SheetPartFile { get; set; }

        public List<InstrumentTO> AllInstruments { get; set; }
        public List<int> SelectedInstrumentsIds { get; set; }

    }
}
