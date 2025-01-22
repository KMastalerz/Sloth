﻿using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class AbandonBugCommand(int bugID): IRequest<IEnumerable<GetAssignmentBugItem>>
{
    public int BugID { get; set; } = bugID;
}
