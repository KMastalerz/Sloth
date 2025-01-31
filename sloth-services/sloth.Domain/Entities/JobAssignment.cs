﻿namespace sloth.Domain.Entities;
public class JobAssignment
{
    public int JobID { get; set; }
    public Guid UserID { get; set; }
    public Guid AssignedByID { get; set; }
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// External properties
    /// </summary>

    public User User { get; set; } = default!;
    public User AssignedBy { get; set; } = default!;
}
