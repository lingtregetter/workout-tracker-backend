﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityHelpers
{
    // extension function
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }

    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
        int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static bool ValidateToken(string jwt, string key, string issuer, string audience)
        // retrieve token from request header
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters(key, issuer, audience);

        SecurityToken validatedToken;
        try
        {
            var principal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);
        }
        catch (SecurityTokenExpiredException)
        {
            // it's ok to be expired
            return true;
        }
        catch (Exception)
        {
            // something else was wrong
            return false;
        }

        return true;
    }

    private static TokenValidationParameters GetValidationParameters(string key, string issuer, string audience)
    {
        return new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true
        };
    }
}