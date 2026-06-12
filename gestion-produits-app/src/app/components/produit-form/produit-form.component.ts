import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProduitService } from '../../services/produit.service';

@Component({
  selector: 'app-produit-form',
  templateUrl: './produit-form.component.html',
  styleUrls: ['./produit-form.component.css']
})
export class ProduitFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  produitId?: number;
  errorMessage = '';
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private produitService: ProduitService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.params['id'];
    if (id) {
      this.produitId = +id;
      this.isEditMode = true;
      this.loadProduit(this.produitId);
    }
  }

  initForm(): void {
    this.form = this.fb.group({
      nom: ['', [Validators.required, Validators.maxLength(100)]],
      description: [''],
      prix: [0, [Validators.required, Validators.min(0.01)]],
      quantite: [0, [Validators.required, Validators.min(0)]]
    });
  }

  loadProduit(id: number): void {
    this.produitService.getProduit(id).subscribe({
      next: (produit) => this.form.patchValue(produit),
      error: (err) => this.errorMessage = err.message
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.isLoading = true;
    const dto = this.form.value;

    const operation = this.isEditMode
      ? this.produitService.updateProduit(this.produitId!, dto)
      : this.produitService.createProduit(dto);

    operation.subscribe({
      next: () => this.router.navigate(['/produits']),
      error: (err) => {
        this.errorMessage = err.message;
        this.isLoading = false;
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/produits']);
  }
}
