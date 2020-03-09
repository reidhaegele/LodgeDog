using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LodgeDogDB.Models;

namespace LodgeDogDB.Context
{
    public partial class mySampleDatabaseContext : DbContext
    {
        public mySampleDatabaseContext()
        {
        }

        public mySampleDatabaseContext(DbContextOptions<mySampleDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:lodgedogsqlserver.database.windows.net,1433;Initial Catalog=mySampleDatabase;Persist Security Info=False;User ID=azureuser;Password=Iwe8a4d@home;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => new { e.TimeStamp, e.Number })
                    .HasName("pk_myConstraint");

                entity.ToTable("bookings");

                entity.Property(e => e.TimeStamp).HasColumnName("TIME_STAMP");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.Property(e => e.Baserateofpay).HasColumnName("BASERATEOFPAY");

                entity.Property(e => e.Checkin)
                    .HasColumnName("CHECKIN")
                    .HasColumnType("datetime");

                entity.Property(e => e.Checkout)
                    .HasColumnName("CHECKOUT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datebookingmade)
                    .HasColumnName("DATEBOOKINGMADE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Guestpassesadded)
                    .HasColumnName("GUESTPASSESADDED")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Guestpassespurchased)
                    .HasColumnName("GUESTPASSESPURCHASED")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Numberofoccupants).HasColumnName("NUMBEROFOCCUPANTS");

                entity.Property(e => e.Pointsused).HasColumnName("POINTSUSED");

                entity.Property(e => e.Primaryguestname)
                    .HasColumnName("PRIMARYGUESTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Property)
                    .HasColumnName("PROPERTY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rci)
                    .HasColumnName("RCI")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reservationpassesneeded)
                    .HasColumnName("RESERVATIONPASSESNEEDED")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reservationpassespurchased)
                    .HasColumnName("RESERVATIONPASSESPURCHASED")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasColumnName("SOURCE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tri).HasColumnName("TRI");

                entity.Property(e => e.Unittype)
                    .HasColumnName("UNITTYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wyndhamconfirmationnumber)
                    .HasColumnName("WYNDHAMCONFIRMATIONNUMBER")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumberNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Number)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookings__NUMBER__1AD3FDA4");
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasKey(e => e.TimeStamp)
                    .HasName("PK__notes__69497B34C219D827");

                entity.ToTable("notes");

                entity.Property(e => e.TimeStamp).HasColumnName("TIME_STAMP");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("NOTE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.Property(e => e.Reason)
                    .HasColumnName("REASON")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumberNavigation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.Number)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Owners>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK__owners__75433F7E12C18153");

                entity.ToTable("owners");

                entity.Property(e => e.Number)
                    .HasColumnName("NUMBER")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Alternateemail)
                    .HasColumnName("ALTERNATEEMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Alternatephonenumber)
                    .HasColumnName("ALTERNATEPHONENUMBER")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Alternatephonenumber2)
                    .HasColumnName("ALTERNATEPHONENUMBER2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("CITY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Expiration)
                    .HasColumnName("EXPIRATION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fullservice)
                    .HasColumnName("FULLSERVICE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Guestpasses).HasColumnName("GUESTPASSES");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Membernumber)
                    .HasColumnName("MEMBERNUMBER")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("PHONENUMBER")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Points).HasColumnName("POINTS");

                entity.Property(e => e.Primarycontact)
                    .HasColumnName("PRIMARYCONTACT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rateofpay).HasColumnName("RATEOFPAY");

                entity.Property(e => e.Rci1)
                    .HasColumnName("RCI1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rci2)
                    .HasColumnName("RCI2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rcimembernumber)
                    .HasColumnName("RCIMEMBERNUMBER")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rcipoints)
                    .HasColumnName("RCIPOINTS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rciyearofuse)
                    .HasColumnName("RCIYEAROFUSE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reservationpasses).HasColumnName("RESERVATIONPASSES");

                entity.Property(e => e.Signupdate)
                    .HasColumnName("SIGNUPDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.State)
                    .HasColumnName("STATE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnName("TIME_STAMP");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Viplevel)
                    .HasColumnName("VIPLEVEL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Wynresemail)
                    .HasColumnName("WYNRESEMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Yearofuse)
                    .HasColumnName("YEAROFUSE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasColumnName("ZIP")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
