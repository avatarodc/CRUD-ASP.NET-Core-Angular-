using FluentValidation;
using GestionProduits.Api.Models.DTOs;

namespace GestionProduits.Api.Validators;

public class CreateProduitDtoValidator : AbstractValidator<CreateProduitDto>
{
    public CreateProduitDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères");

        RuleFor(x => x.Prix)
            .GreaterThan(0).WithMessage("Le prix doit être supérieur à 0");

        RuleFor(x => x.Quantite)
            .GreaterThanOrEqualTo(0).WithMessage("La quantité ne peut pas être négative");
    }
}

public class UpdateProduitDtoValidator : AbstractValidator<UpdateProduitDto>
{
    public UpdateProduitDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères");

        RuleFor(x => x.Prix)
            .GreaterThan(0).WithMessage("Le prix doit être supérieur à 0");

        RuleFor(x => x.Quantite)
            .GreaterThanOrEqualTo(0).WithMessage("La quantité ne peut pas être négative");
    }
}
