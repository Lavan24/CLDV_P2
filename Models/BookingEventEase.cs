using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseApplication.Models;

public partial class BookingEventEase
{
    [Required]
    public int BookingId { get; set; }

    [Required]
    public int? VenueId { get; set; }

    [Required]
    public int? EventId { get; set; }

    [Required]
    public DateOnly? BookingDate { get; set; }

    [Required]
    public TimeOnly? Booking_Time { get; set; }

    public virtual EventEventEase? Event { get; set; }

    public virtual VenueEventEase? Venue { get; set; }
}
