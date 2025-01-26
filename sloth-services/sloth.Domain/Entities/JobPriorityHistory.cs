﻿namespace sloth.Domain.Entities;
public class JobPriorityHistory
{
    public int JobID { get; set; }
    public int? PriorityID { get; set; } = null;
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }
    public string Action { get; set; } = default!;

    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
    public Priority? Priority { get; set; } = null;
}
