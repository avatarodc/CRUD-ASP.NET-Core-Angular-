using AutoMapper;
using GestionProduits.Api.Models.DTOs;
using GestionProduits.Api.Models.Entities;

namespace GestionProduits.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produit, ProduitDto>();
        CreateMap<CreateProduitDto, Produit>();
        CreateMap<UpdateProduitDto, Produit>();
    }
}
