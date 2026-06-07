using GestionProduits.Api.Models.DTOs;
using GestionProduits.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionProduits.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProduitsController : ControllerBase
{
    private readonly IProduitService _service;
    private readonly ILogger<ProduitsController> _logger;

    public ProduitsController(IProduitService service, ILogger<ProduitsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProduitDto>>> GetProduits()
    {
        var produits = await _service.GetAllAsync();
        return Ok(produits);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProduitDto>> GetProduit(int id)
    {
        var produit = await _service.GetByIdAsync(id);
        return produit == null ? NotFound(new { message = $"Produit {id} non trouvé" }) : Ok(produit);
    }

    [HttpPost]
    public async Task<ActionResult<ProduitDto>> CreateProduit(CreateProduitDto dto)
    {
        var produit = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetProduit), new { id = produit.Id }, produit);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProduitDto>> UpdateProduit(int id, UpdateProduitDto dto)
    {
        var produit = await _service.UpdateAsync(id, dto);
        return produit == null ? NotFound(new { message = $"Produit {id} non trouvé" }) : Ok(produit);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduit(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = $"Produit {id} non trouvé" });
    }
}
