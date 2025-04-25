using System;
using System.Collections.Generic;

namespace EventEaseApplication.Models;

public partial class VenueEventEase
{
    public int VenueId { get; set; }

    public string? VenueName { get; set; }

    public string? VenueLocation { get; set; }

    public int? VenueCapacity { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<BookingEventEase> BookingEventEases { get; set; } = new List<BookingEventEase>();
}
