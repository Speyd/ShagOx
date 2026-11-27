namespace ShagOxServer.Application.DTOs.Auth.Users.UserSessions;
public sealed record SessionInfo
(
    string Browser,
    string BrowserVerison,
    string IpAddress
);
