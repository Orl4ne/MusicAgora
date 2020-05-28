using Library.DAL;
using MusicAgora.Common.Library.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.Services.MusicianUC
{
    public partial class Musician : IMusicianUC
    {
        private LibraryContext context;
    }
}
