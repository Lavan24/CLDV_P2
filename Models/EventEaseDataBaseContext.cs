using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventEaseApplication.Models;

public partial class EventEaseDataBaseContext : DbContext
{
    public EventEaseDataBaseContext()
    {
    }

    public EventEaseDataBaseContext(DbContextOptions<EventEaseDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingEventEase> BookingEventEases { get; set; }

    public virtual DbSet<EventEventEase> EventEventEases { get; set; }

    public virtual DbSet<VenueEventEase> VenueEventEases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:eventease-application.database.windows.net,1433;Initial Catalog=EventEaseDataBase;Persist Security Info=False;User ID=Lavanaya;Password=D3athN0te;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\r\n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingEventEase>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking___35ABFDE0829A066F");

            entity.ToTable("Booking_EventEase");

            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.BookingDate).HasColumnName("Booking_Date");
            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.VenueId).HasColumnName("Venue_ID");

            entity.HasOne(d => d.Event).WithMany(p => p.BookingEventEases)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Booking_E__Event__4E88ABD4");

            entity.HasOne(d => d.Venue).WithMany(p => p.BookingEventEases)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Booking_E__Venue__4D94879B");
        });

        modelBuilder.Entity<EventEventEase>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event_Ev__FD6BEFE41EECF377");

            entity.ToTable("Event_EventEase");

            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.EventDate).HasColumnName("Event_Date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Event_Description");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Event_Name");
        });

        modelBuilder.Entity<VenueEventEase>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue_Ev__4A99C4B90370BE56");

            entity.ToTable("Venue_EventEase");

            entity.Property(e => e.VenueId).HasColumnName("Venue_ID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Image_URL");
            entity.Property(e => e.VenueCapacity).HasColumnName("Venue_Capacity");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Venue_Location");
            entity.Property(e => e.VenueName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Venue_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
