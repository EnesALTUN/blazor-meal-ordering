namespace MealOrdering.Core.Entities.Configuration;

public class JwtSetting
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string SecurityKey { get; set; }

    public int ExpiryInDay { get; set; }
}