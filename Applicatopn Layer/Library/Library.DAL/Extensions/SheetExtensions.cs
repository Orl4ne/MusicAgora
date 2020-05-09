﻿using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class SheetExtensions
    {
        public static SheetTO ToTransferObject (this SheetEF Sheet)
        {
            if (Sheet is null)
                throw new ArgumentNullException(nameof(Sheet));

            return new SheetTO
            {
                Id = Sheet.Id,
                Name = Sheet.Name,
                SheetParts = Sheet.SheetParts.Select(x=> x.ToTransferObject()).ToList(),
                Arranger = Sheet.Arranger,
                Category = Sheet.Category.ToTransferObject(),
                Composer = Sheet.Composer,
                IsCurrent = Sheet.IsCurrent,
                IsGarde = Sheet.IsGarde,
                IsIndependance = Sheet.IsIndependance,
            };
        }

        public static SheetEF ToEF (this SheetTO Sheet)
        {
            if (Sheet is null)
                throw new ArgumentNullException(nameof(Sheet));

            return new SheetEF
            {
                Id = Sheet.Id,
                Name = Sheet.Name,
                SheetParts = Sheet.SheetParts.Select(x => x.ToEF()).ToList(),
                Arranger = Sheet.Arranger,
                Category = Sheet.Category.ToEF(),
                Composer = Sheet.Composer,
                IsCurrent = Sheet.IsCurrent,
                IsGarde = Sheet.IsGarde,
                IsIndependance = Sheet.IsIndependance,
            };
        }
    }
}