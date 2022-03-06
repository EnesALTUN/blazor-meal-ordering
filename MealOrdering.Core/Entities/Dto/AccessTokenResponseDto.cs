namespace MealOrdering.Core.Entities.Dto;

public class AccessTokenResponseDto
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}