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
                new InstrumentEF { Name = "Flûte/Piccolo"},
                new InstrumentEF { Name = "Hautbois"},
                new InstrumentEF { Name = "Trompette/Cornet"},
                new InstrumentEF { Name = "Bugle"},
                new InstrumentEF { Name = "Clarinette"},
                new InstrumentEF { Name = "Saxophone"},
                new InstrumentEF { Name = "Cor"},
                new InstrumentEF { Name = "Baryton/Euphonium"},
                new InstrumentEF { Name = "Trombone/Buccin/Serpent"},
                new InstrumentEF { Name = "Clarinette Basse"},
                new InstrumentEF { Name = "Basson"},
                new InstrumentEF { Name = "Tuba"},
                new InstrumentEF { Name = "Basse Ut"},
                new InstrumentEF { Name = "Percussions"},
                new InstrumentEF { Name = "Autre"},
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
