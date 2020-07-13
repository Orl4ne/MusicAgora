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
                Category = Sheet.Category.ToTransferObject(),
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
                Category = Sheet.Category.ToEF(),
                Composer = Sheet.Composer,
                IsCurrent = Sheet.IsCurrent,
                IsGarde = Sheet.IsGarde,
                IsIndependance = Sheet.IsIndependance,
            };
        }

        public static SheetEF ToTrackedEF(this SheetTO Sheet, SheetEF SheetToModify)
        {
            if (SheetToModify is null)
                throw new ArgumentNullException(nameof(SheetToModify));
            if (Sheet is null)
                throw new ArgumentNullException(nameof(Sheet));

            SheetToModify.Id = Sheet.Id;
            SheetToModify.Name = Sheet.Name;
            SheetToModify.Arranger = Sheet.Arranger;
            SheetToModify.Category = Sheet.Category.ToTrackedEF(SheetToModify.Category);
            SheetToModify.Composer = Sheet.Composer;
            SheetToModify.IsCurrent = Sheet.IsCurrent;
            SheetToModify.IsGarde = Sheet.IsGarde;
            SheetToModify.IsIndependance = Sheet.IsIndependance;

            return SheetToModify;
        }
    }
}
