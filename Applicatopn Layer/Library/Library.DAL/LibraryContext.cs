﻿using System;
using System.Collections.Generic;
using System.Text;
using Library.DAL.Auth;
using Library.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL
{
    public class LibraryContext : IdentityDbContext<User, AccessRight, int> 
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
                optionsBuilder.UseSqlite(@"Data Source=musicagora.db;");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInstrumentEF>().HasKey(sc => new { sc.UserID, sc.InstrumentId });

            modelBuilder.Entity<UserInstrumentEF>()
                .HasOne<User>(ui => ui.User)
                .WithMany(u => u.UserInstruments)
                .HasForeignKey(ui => ui.UserID);

            modelBuilder.Entity<UserInstrumentEF>()
                .HasOne<InstrumentEF>(ui => ui.Instrument)
                .WithMany(i => i.UserInstruments)
                .HasForeignKey(ui => ui.InstrumentId);
        }
       
        public DbSet<CategoryEF> Categories { get; set; }
        public DbSet<InstrumentEF> Instruments { get; set; }
        public DbSet<SheetEF> Sheets { get; set; }
        public DbSet<SheetPartEF> SheetParts { get; set; }
        public DbSet<UserInstrumentEF> UserInstruments { get; set; }
    }
}
