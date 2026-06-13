using GestionProduits.Api.Models.Entities;

namespace GestionProduits.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
