using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class SheetExtensions
    {
        public static SheetTO ToTransferObject(this SheetEF Sheet)
        {
            if (Sheet is null)
                throw new ArgumentNullException(nameof(Sheet));

            return new SheetTO
            {
                Id = Sheet.Id,
                Name = Sheet.Name,
                Arranger = Sheet.Arranger,
                CategoryId = Sheet.CategoryId,
                Composer = Sheet.Composer,
                IsCurrent = Sheet.IsCurrent,
                IsGarde = Sheet.IsGarde,
                IsIndependance = Sheet.IsIndependance,
            };
        }

        public static SheetEF ToEF(this SheetTO Sheet)
        {
            if (Sheet is null)
                throw new ArgumentNullException(nameof(Sheet));

            return new SheetEF
            {
                Id = Sheet.Id,
                Name = Sheet.Name,
                Arranger = Sheet.Arranger,
                CategoryId = Sheet.CategoryId,
                Composer = Sheet.Composer,
                IsCurrent = Sheet.IsCurrent,
                IsGarde = Sheet.IsGarde,
                IsIndependance = Sheet.IsIndependance,
            };
        }
    }
}
