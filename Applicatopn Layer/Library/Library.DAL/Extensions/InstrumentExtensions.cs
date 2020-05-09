﻿using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Extensions
{
    public static class InstrumentExtensions
    {
        public static InstrumentTO ToTransferObject(this InstrumentEF Instrument)
        {
            if (Instrument is null)
                throw new ArgumentNullException(nameof(Instrument));

            return new InstrumentTO
            {
                Id = Instrument.Id,
                Name = Instrument.Name,
            };
        }

        public static InstrumentEF ToEF(this InstrumentTO Instrument)
        {
            if (Instrument is null)
                throw new ArgumentNullException(nameof(Instrument));

            var instruEf = new InstrumentEF
            {
                Id = Instrument.Id,
                Name = Instrument.Name,
            };
            return instruEf;
        }
    }
}