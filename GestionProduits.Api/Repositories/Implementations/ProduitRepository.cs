using GestionProduits.Api.Data;
using GestionProduits.Api.Models.Entities;
using GestionProduits.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Api.Repositories.Implementations;

public class ProduitRepository : IProduitRepository
{
    private readonly AppDbContext _context;

    public ProduitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produit>> GetAllAsync()
        => await _context.Produits.ToListAsync();

    public async Task<Produit?> GetByIdAsync(int id)
        => await _context.Produits.FindAsync(id);

    public async Task<Produit> CreateAsync(Produit produit)
    {
        _context.Produits.Add(produit);
        await _context.SaveChangesAsync();
        return produit;
    }

    public async Task<Produit?> UpdateAsync(int id, Produit produit)
    {
        var existing = await _context.Produits.FindAsync(id);
        if (existing == null) return null;

        existing.Nom = produit.Nom;
        existing.Description = produit.Description;
        existing.Prix = produit.Prix;
        existing.Quantite = produit.Quantite;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produit = await _context.Produits.FindAsync(id);
        if (produit == null) return false;

        _context.Produits.Remove(produit);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Produits.AnyAsync(p => p.Id == id);
}
