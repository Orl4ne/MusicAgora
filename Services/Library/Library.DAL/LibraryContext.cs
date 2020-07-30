using System;
using System.Collections.Generic;
using System.Text;
using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using MusicAgora.Common.Library.TransferObjects;

namespace Library.DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=musicagoraLibrary.db;");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<UserInstruEF>().HasKey(ui => new { ui.LibUserId, ui.InstrumentId });

            modelBuilder.Entity<UserInstruEF>()
                .HasOne<LibUserEF>(ui => ui.LibUser)
                .WithMany(i => i.UserInstruments)
                .HasForeignKey(ui => ui.LibUserId);

            modelBuilder.Entity<UserInstruEF>()
                .HasOne<InstrumentEF>(ui => ui.Instrument)
                .WithMany(u => u.UserInstruments)
                .HasForeignKey(ui => ui.InstrumentId);

            var instruments = new[]
           {
            new InstrumentEF { Name = "Direction"},
            new InstrumentEF { Name = "Flûte"},
            new InstrumentEF { Name = "Trompette"},
            new InstrumentEF { Name = "Saxophone Alto"},
            new InstrumentEF { Name = "Saxophone Tenor"},
            new InstrumentEF { Name = "Saxophone Soprano"},
            new InstrumentEF { Name = "Saxophone Baryton"},
            new InstrumentEF { Name = "Tuba"},
            new InstrumentEF { Name = "Tuba Basse"},
            new InstrumentEF { Name = "Trombone"},
            new InstrumentEF { Name = "Clarinette Sib"},
            new InstrumentEF { Name = "Clarinette Mib"},
            new InstrumentEF { Name = "Piccolo"},
            new InstrumentEF { Name = "Serpent"},
            new InstrumentEF { Name = "Buccin"},
            new InstrumentEF { Name = "Basse/Contrebasse Ut"},
            new InstrumentEF { Name = "Percussions"},

            };

            for (int i = 0; i < instruments.Length; i++)
            {
                instruments[i].Id = i + 1;
                modelBuilder.Entity<InstrumentEF>().HasData(instruments[i]);
            }
        }

        public DbSet<CategoryEF> Categories { get; set; }
        public DbSet<InstrumentEF> Instruments { get; set; }
        public DbSet<SheetEF> Sheets { get; set; }
        public DbSet<SheetPartEF> SheetParts { get; set; }
        public DbSet<LibUserEF> LibraryUsers { get; set; }
        public DbSet<UserInstruEF> UserInstruments { get; set; }
    }
}
