using AutoMapper;
using GestionProduits.Api.Models.DTOs;
using GestionProduits.Api.Models.Entities;
using GestionProduits.Api.Repositories.Interfaces;
using GestionProduits.Api.Services.Interfaces;

namespace GestionProduits.Api.Services.Implementations;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProduitService> _logger;

    public ProduitService(IProduitRepository repository, IMapper mapper, ILogger<ProduitService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProduitDto>> GetAllAsync()
    {
        _logger.LogInformation("Récupération de tous les produits");
        var produits = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProduitDto>>(produits);
    }

    public async Task<ProduitDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Récupération du produit id={Id}", id);
        var produit = await _repository.GetByIdAsync(id);
        return produit == null ? null : _mapper.Map<ProduitDto>(produit);
    }

    public async Task<ProduitDto> CreateAsync(CreateProduitDto dto)
    {
        var produit = _mapper.Map<Produit>(dto);
        produit.CreatedAt = DateTime.UtcNow;
        produit.UpdatedAt = DateTime.UtcNow;

        var created = await _repository.CreateAsync(produit);
        _logger.LogInformation("Produit créé avec succès, id={Id}", created.Id);
        return _mapper.Map<ProduitDto>(created);
    }

    public async Task<ProduitDto?> UpdateAsync(int id, UpdateProduitDto dto)
    {
        var produit = _mapper.Map<Produit>(dto);
        var updated = await _repository.UpdateAsync(id, produit);

        if (updated == null)
        {
            _logger.LogWarning("Produit id={Id} non trouvé pour la mise à jour", id);
            return null;
        }

        _logger.LogInformation("Produit mis à jour avec succès, id={Id}", id);
        return _mapper.Map<ProduitDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            _logger.LogWarning("Produit id={Id} non trouvé pour la suppression", id);
        else
            _logger.LogInformation("Produit supprimé avec succès, id={Id}", id);
        return deleted;
    }
}
