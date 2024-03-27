using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HotelBookingSystem.DAL;
using HotelBookingSystem.Models.Entities;

namespace HotelBookingSystem.DAL.Contexts
{
    public partial class HotelBookingContext : DbContext
    {
        public HotelBookingContext()
        {
        }

        public HotelBookingContext(DbContextOptions<HotelBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aminity> Aminities { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<HotelAminity> HotelAminities { get; set; } = null!;
        public virtual DbSet<HotelDescription> HotelDescriptions { get; set; } = null!;
        public virtual DbSet<HotelPicture> HotelPictures { get; set; } = null!;
        public virtual DbSet<HotelRoom> HotelRooms { get; set; } = null!;
        public virtual DbSet<OccupantDetail> OccupantDetails { get; set; } = null!;
        public virtual DbSet<RoomPicture> RoomPictures { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aminity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AminityDescription).IsUnicode(false);

                entity.Property(e => e.AminityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasIndex(e => e.BookingNo, "AK_Booking_Booking_no")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApprovedBy).HasColumnName("Approved by");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("date")
                    .HasColumnName("Approved  date");

                entity.Property(e => e.BookingNo)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Booking no");

                entity.Property(e => e.CreatedBy).HasColumnName("Created by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Created date");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End date");

                entity.Property(e => e.HotelId).HasColumnName("Hotel id");

                entity.Property(e => e.RoomId).HasColumnName("Room id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start date");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Processing')");

                entity.Property(e => e.UdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Udated date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated by");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.BookingApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_Booking_A_User");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BookingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Booking_C_User");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Booking_Hotel");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Booking_HotelRooms");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.BookingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Booking_U_User");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.HotelRating)
                    .HasColumnType("decimal(1, 0)")
                    .HasColumnName("Hotel Rating");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pincode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Active')");
            });

            modelBuilder.Entity<HotelAminity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Aminity)
                    .WithMany(p => p.HotelAminities)
                    .HasForeignKey(d => d.Aminityid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelAminities_Aminities");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelAminities)
                    .HasForeignKey(d => d.Hotelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelAminities_Hotel");
            });

            modelBuilder.Entity<HotelDescription>(entity =>
            {
                entity.ToTable("HotelDescription");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelDescriptions)
                    .HasForeignKey(d => d.Hotelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Hotel");
            });

            modelBuilder.Entity<HotelPicture>(entity =>
            {
                entity.ToTable("Hotel Picture");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.HotelId).HasColumnName("Hotel_Id");

                entity.Property(e => e.ImageEndpoint)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Image Endpoint");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelPictures)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel Picture_Hotel");
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BedSize)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Bed size");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.HotelId).HasColumnName("Hotel Id");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Room name");

                entity.Property(e => e.RoomType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Room Type");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelRooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelRooms_Hotel");
            });

            modelBuilder.Entity<OccupantDetail>(entity =>
            {
                entity.ToTable("Occupant Details");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BookingId).HasColumnName("Booking id");

                entity.Property(e => e.CreatedBy).HasColumnName("Created by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Created date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last name");

                entity.Property(e => e.UdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Udated date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated by");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.OccupantDetails)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupant Details_Booking");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.OccupantDetailCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupant_C_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.OccupantDetailUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupant_U_User");
            });

            modelBuilder.Entity<RoomPicture>(entity =>
            {
                entity.ToTable("Room Picture");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageEndpoint)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Image Endpoint");

                entity.Property(e => e.RoomId).HasColumnName("Room_Id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomPictures)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room Picture_Room");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "AK_User_Username")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Created date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First name");

                entity.Property(e => e.ImageEndPoint)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Image EndPoint");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last name");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Active')");

                entity.Property(e => e.Type)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("type")
                    .HasDefaultValueSql("('Customer')");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
