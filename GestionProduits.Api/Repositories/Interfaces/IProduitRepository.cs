using GestionProduits.Api.Models.Entities;

namespace GestionProduits.Api.Repositories.Interfaces;

public interface IProduitRepository
{
    Task<IEnumerable<Produit>> GetAllAsync();
    Task<Produit?> GetByIdAsync(int id);
    Task<Produit> CreateAsync(Produit produit);
    Task<Produit?> UpdateAsync(int id, Produit produit);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
