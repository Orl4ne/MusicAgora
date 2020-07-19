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

        public static SheetEF UpdateFromDetached(this SheetEF AttachedEF, SheetEF DetachedEF)
        {
            if (AttachedEF is null)
                throw new ArgumentNullException(nameof(AttachedEF));
            if (DetachedEF is null)
                throw new ArgumentNullException(nameof(DetachedEF));
            if ((AttachedEF != default) && (DetachedEF != default))
            {
                AttachedEF.Id = DetachedEF.Id;
                AttachedEF.Name = DetachedEF.Name;
                AttachedEF.Arranger = DetachedEF.Arranger;
                AttachedEF.Category = DetachedEF.Category;
                AttachedEF.Composer = DetachedEF.Composer;
                AttachedEF.IsCurrent = DetachedEF.IsCurrent;
                AttachedEF.IsGarde = DetachedEF.IsGarde;
                AttachedEF.IsIndependance = DetachedEF.IsIndependance;
            }
            return AttachedEF;
        }
    }
}
