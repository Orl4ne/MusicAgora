using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicAgora.Common.Library.Interfaces.UseCases
{
    public interface ILibrarianUC
    {
        SheetTO CreateANewSheet(SheetTO Sheet);
        SheetPartTO UploadSheetPartInSheet(SheetPartTO SheetPart, Stream file);
        CategoryTO AddNewCategory(CategoryTO category);
    }
}
