using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAgora.WebUx.MVC.Models
{
    public class LibraryVM
    {
        public SheetTO Sheet { get; set; }
        public List<CategoryTO> AllCategories { get; set; }
        public string SelectedCategory { get; set; }

    }
}
