using Library.DAL.Entities;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //LibUserIds = Instrument.UserInstruments?.Select(x => x.LibUserId).ToList()
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
            //instruEf.UserInstruments = Instrument.LibUserIds?.Select(x => new UserInstruEF
            //{
            //    InstrumentId = instruEf.Id,
            //    LibUserId = x,
            //}).ToList();

            return instruEf;
        }

        public static InstrumentEF ToTrackedEF(this InstrumentTO Instrument, InstrumentEF InstrumentToModify)
        {
            if (InstrumentToModify is null)
                throw new ArgumentNullException(nameof(InstrumentToModify));
            if (Instrument is null)
                throw new ArgumentNullException(nameof(Instrument));

            InstrumentToModify.Id = Instrument.Id;
            InstrumentToModify.Name = Instrument.Name;

            return InstrumentToModify;
        }
    }
}
