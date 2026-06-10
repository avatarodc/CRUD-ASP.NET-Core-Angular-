export interface Produit {
  id: number;
  nom: string;
  description: string;
  prix: number;
  quantite: number;
  createdAt: string;
  updatedAt: string;
}

export interface CreateProduitDto {
  nom: string;
  description: string;
  prix: number;
  quantite: number;
}

export interface UpdateProduitDto {
  nom: string;
  description: string;
  prix: number;
  quantite: number;
}
