using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseApplication.Models;

public partial class EventEventEase
{
    [Required]
    public int EventId { get; set; }

    [Required]
    public string? EventName { get; set; }

    [Required]
    public DateOnly? EventDate { get; set; }

    [Required]
    public TimeOnly? Event_Time { get; set; }

    [Required]
    public string? EventDescription { get; set; }

    public virtual ICollection<BookingEventEase> BookingEventEases { get; set; } = new List<BookingEventEase>();
}
