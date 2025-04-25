using System;
using System.Collections.Generic;

namespace EventEaseApplication.Models;

public partial class EventEventEase
{
    public int EventId { get; set; }

    public string? EventName { get; set; }

    public DateOnly? EventDate { get; set; }

    public string? EventDescription { get; set; }

    public virtual ICollection<BookingEventEase> BookingEventEases { get; set; } = new List<BookingEventEase>();
}
