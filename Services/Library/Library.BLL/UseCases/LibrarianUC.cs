using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.IO;
using System.Reflection.Metadata;

namespace Library.BLL.UseCases
{
    public class LibrarianUC : ChiefUC, ILibrarianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public LibrarianUC(ILibraryUnitOfWork iLibraryUnitOfWork, IConfiguration config) : base (iLibraryUnitOfWork, config)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
            this.config = config;
        }
        #endregion

        public SheetTO CreateANewSheet(SheetTO Sheet)
        {
            var root = config.GetValue<string>("DataPath");
            var path = $"{root}{Sheet.Name}";
            Directory.CreateDirectory(path);
            return unitOfWork.SheetRepository.Add(Sheet);
        }

        public SheetPartTO UploadSheetPartInSheet(SheetPartTO SheetPart, MemoryStream file)
        {
            var root = config.GetValue<string>("DataPath");
            var path = $@"{root}{SheetPart.Sheet.Name}\\{SheetPart.Sheet.Name}-{SheetPart.Instrument.Name}.pdf";
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch
            {
                throw new Exception("Il y a eu une erreur lors de l'enregistrement du fichier");
            }
            SheetPart.Path = path;
            unitOfWork.SheetPartRepository.Update(SheetPart);
            unitOfWork.SaveChanges();

            return SheetPart;
        }
    }
}
