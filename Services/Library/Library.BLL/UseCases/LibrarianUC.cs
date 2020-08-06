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
    public class LibrarianUC : ILibrarianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public LibrarianUC(ILibraryUnitOfWork iLibraryUnitOfWork, IConfiguration config) 
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
            this.config = config;
        }

        public CategoryTO AddNewCategory(CategoryTO category)
        {
            if (category == null || category.Name.Trim().Length<1 )
            {
                throw new ArgumentNullException();
            };
            var addedCategory = unitOfWork.CategoryRepository.Add(category);
            return addedCategory;
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
            var completePath = $@"{root}{SheetPart.Sheet.Name}\\{SheetPart.Sheet.Name}-{SheetPart.Instrument.Name}-{SheetPart.Part}.pdf";
            try
            {
                using (FileStream fs = new FileStream(completePath, FileMode.OpenOrCreate))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch
            {
                throw new Exception("Il y a eu une erreur lors de l'enregistrement du fichier");
            }
            var path = $@"{SheetPart.Sheet.Name}\\{SheetPart.Sheet.Name}-{SheetPart.Instrument.Name}-{SheetPart.Part}.pdf";
            SheetPart.Path = path;
            unitOfWork.SheetPartRepository.Update(SheetPart);
            unitOfWork.SaveChanges();

            return SheetPart;
        }
    }
}
