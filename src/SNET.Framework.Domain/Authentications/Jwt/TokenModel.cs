namespace SNET.Framework.Domain.Authentications.Jwt;

public record TokenModel(string Token, DateTime Expires, Dictionary<string, string> Claims);

