using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseApplication.Models;

public partial class VenueEventEase
{
    [Required]
    public int VenueId { get; set; }

    [Required]
    public string? VenueName { get; set; }

    [Required]
    public string? VenueLocation { get; set; }

    [Required]
    public int? VenueCapacity { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    public virtual ICollection<BookingEventEase> BookingEventEases { get; set; } = new List<BookingEventEase>();
}
