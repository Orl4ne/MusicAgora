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
            //if (Instrument is null)
            //    throw new ArgumentNullException(nameof(Instrument));

            return new InstrumentTO
            {
                Id = Instrument.Id,
                Name = Instrument.Name,
                UserIds = Instrument.UserInstruments.Where(u => u.InstrumentId == Instrument.Id).Select(u => u.UserID).ToList(),
            };
        }

        public static InstrumentEF ToEF(this InstrumentTO Instrument)
        {
            //if (Instrument is null)
            //    throw new ArgumentNullException(nameof(Instrument));

            var instruEf = new InstrumentEF
            {
                Id = Instrument.Id,
                Name = Instrument.Name,
                
            };
            if (Instrument.UserIds == null)
            {
                return instruEf;
            }
            instruEf.UserInstruments = new List<UserInstrumentEF>();

            return instruEf;
        }
    }
}
