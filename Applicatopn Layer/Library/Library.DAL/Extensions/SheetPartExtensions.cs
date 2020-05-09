﻿using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class SheetPartExtensions
    {
        public static SheetPartTO ToTransferObject ( this SheetPartEF SheetPart)
        {
            if (SheetPart is null)
                throw new ArgumentNullException(nameof(SheetPart));

            return new SheetPartTO
            {
                Id = SheetPart.Id,
                Path = SheetPart.Path,
                Sheet = SheetPart.Sheet.ToTransferObject(),
                Instrument = SheetPart.Instrument.ToTransferObject(),
            }; 
        }

        public static SheetPartEF ToEF(this SheetPartTO SheetPart)
        {
            if (SheetPart is null)
                throw new ArgumentNullException(nameof(SheetPart));

            return new SheetPartEF
            {
                Id = SheetPart.Id,
                Path = SheetPart.Path,
                Sheet = SheetPart.Sheet.ToEF(),
                Instrument = SheetPart.Instrument.ToEF(),
            };
        }
    }
}