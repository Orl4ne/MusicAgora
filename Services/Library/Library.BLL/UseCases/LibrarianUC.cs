using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

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
            if (category == null || category.Name.Trim().Length < 1)
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
            var reg = new Regex("\\W");
            var path = $"{root}{reg.Replace(Sheet.Name, "_")}";
            Directory.CreateDirectory(path);
            return unitOfWork.SheetRepository.Add(Sheet);
        }

        public SheetPartTO UploadSheetPartInSheet(SheetPartTO SheetPart, Stream file)
        {
            var root = config.GetValue<string>("DataPath");
            var reg = new Regex("\\W");
            var path = $@"{reg.Replace(SheetPart.Sheet.Name, "_")}\\{reg.Replace(SheetPart.Sheet.Name, "_")}-{reg.Replace(SheetPart.Part, "_")}.pdf";
            var completePath = $@"{root}{path}";
            if (File.Exists(completePath) == false)
            {
                using (FileStream fs = new FileStream(completePath, FileMode.OpenOrCreate))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            SheetPart.Path = path;
            unitOfWork.SheetPartRepository.Add(SheetPart);

            return SheetPart;
        }
    }
}
