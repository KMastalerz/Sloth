﻿namespace Sloth.Application.UserIdentity;
public record class CurrentUser(string UserID, string Email, string UserName, string? UserGroup, string? UserRole, Guid UserGuid)
{
    public bool IsInRole(string role) => role == UserRole;
    public bool IsInUserGroup(string group) => group == UserGroup;
}
