using MealOrdering.Core.Entities.Configuration;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Core.Utilities.Results.Concrete;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealOrdering.Core.Utilities.Security.Jwt;

public class JwtHelper : IJwtHelper
{
    private readonly ILogger<JwtHelper> _logger;
    private readonly JwtSetting _jwtSetting;

    public JwtHelper(ILogger<JwtHelper> logger, IOptions<JwtSetting> jwtSetting)
    {
        _logger = logger;
        _jwtSetting = jwtSetting.Value;
    }

    public IDataResult<AccessTokenResponseDto> CreateToken(UserDto user)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_jwtSetting.ExpiryInDay);

            var token = new JwtSecurityToken(_jwtSetting.Issuer, _jwtSetting.Audience, SetClaim(user), null, expires, credentials);

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            AccessTokenResponseDto response = new()
            {
                Token = tokenStr,
                Expiration = expires
            };

            return new SuccessDataResult<AccessTokenResponseDto>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ErrorDataResult<AccessTokenResponseDto>("An error occurred while generating the token.");
        }
    }

    private static IEnumerable<Claim> SetClaim(UserDto user)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };
    }
}