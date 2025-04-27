using System;
using System.Collections.Generic;

namespace EventEaseApplication.Models;

public partial class BookingEventEase
{
    public int BookingId { get; set; }

    public int? VenueId { get; set; }

    public int? EventId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public TimeOnly? Booking_Time { get; set; }

    public virtual EventEventEase? Event { get; set; }

    public virtual VenueEventEase? Venue { get; set; }
}
