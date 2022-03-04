using MealOrdering.Core.Entities.Configuration;
using MealOrdering.Core.Entities.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealOrdering.Core.Utilities.Security.Jwt;

public class JwtHelper : IJwtHelper
{
    private readonly JwtSetting _jwtSetting;

    public JwtHelper(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    public AccessTokenResponseDto CreateToken(UserDto user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(_jwtSetting.ExpiryInDay);

        var token = new JwtSecurityToken(_jwtSetting.Issuer, _jwtSetting.Audience, SetClaim(user), null, expires, credentials);

        string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return new AccessTokenResponseDto
        {
            Token = tokenStr,
            Expiration = expires
        };
    }

    private static IEnumerable<Claim> SetClaim(UserDto user)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.EmailAddress)
        };
    }
}