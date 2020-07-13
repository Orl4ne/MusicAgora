using System;
using System.Collections.Generic;
using System.Text;
using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
        }
       
        public DbSet<CategoryEF> Categories { get; set; }
        public DbSet<InstrumentEF> Instruments { get; set; }
        public DbSet<SheetEF> Sheets { get; set; }
        public DbSet<SheetPartEF> SheetParts { get; set; }
    }
}
