import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Produit } from '../../models/produit.model';
import { ProduitService } from '../../services/produit.service';

@Component({
  selector: 'app-produit-list',
  templateUrl: './produit-list.component.html',
  styleUrls: ['./produit-list.component.css']
})
export class ProduitListComponent implements OnInit {
  produits: Produit[] = [];
  errorMessage = '';
  successMessage = '';

  constructor(
    private produitService: ProduitService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProduits();
  }

  loadProduits(): void {
    this.produitService.getProduits().subscribe({
      next: (produits) => {
        this.produits = produits;
        this.errorMessage = '';
      },
      error: (err) => this.errorMessage = err.message
    });
  }

  editProduit(id: number): void {
    this.router.navigate(['/produits/edit', id]);
  }

  deleteProduit(id: number): void {
    if (!confirm('Êtes-vous sûr de vouloir supprimer ce produit ?')) return;

    this.produitService.deleteProduit(id).subscribe({
      next: () => {
        this.successMessage = 'Produit supprimé avec succès';
        this.errorMessage = '';
        this.loadProduits();
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (err) => this.errorMessage = err.message
    });
  }
}
