using GestionProduits.Api.Models.DTOs;

namespace GestionProduits.Api.Services.Interfaces;

public interface IProduitService
{
    Task<IEnumerable<ProduitDto>> GetAllAsync();
    Task<ProduitDto?> GetByIdAsync(int id);
    Task<ProduitDto> CreateAsync(CreateProduitDto dto);
    Task<ProduitDto?> UpdateAsync(int id, UpdateProduitDto dto);
    Task<bool> DeleteAsync(int id);
}
