namespace GestionProduits.Api.Models.DTOs;

public record RegisterDto(string FirstName, string LastName, string Email, string Password);
public record LoginDto(string Email, string Password);
public record RefreshTokenDto(string RefreshToken);
public record LogoutDto(string RefreshToken);

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    UserDto User);

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    bool IsActive,
    DateTime CreatedAt);
