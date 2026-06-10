import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Produit, CreateProduitDto, UpdateProduitDto } from '../models/produit.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProduitService {
  private apiUrl = `${environment.apiUrl}/api/produits`;

  constructor(private http: HttpClient) {}

  getProduits(): Observable<Produit[]> {
    return this.http.get<Produit[]>(this.apiUrl).pipe(
      catchError(error => {
        console.error('Erreur lors de la récupération des produits', error);
        return throwError(() => error);
      })
    );
  }

  getProduit(id: number): Observable<Produit> {
    return this.http.get<Produit>(`${this.apiUrl}/${id}`).pipe(
      catchError(error => {
        console.error(`Erreur lors de la récupération du produit ${id}`, error);
        return throwError(() => error);
      })
    );
  }

  createProduit(dto: CreateProduitDto): Observable<Produit> {
    return this.http.post<Produit>(this.apiUrl, dto).pipe(
      catchError(error => {
        console.error('Erreur lors de la création du produit', error);
        return throwError(() => error);
      })
    );
  }

  updateProduit(id: number, dto: UpdateProduitDto): Observable<Produit> {
    return this.http.put<Produit>(`${this.apiUrl}/${id}`, dto).pipe(
      catchError(error => {
        console.error(`Erreur lors de la mise à jour du produit ${id}`, error);
        return throwError(() => error);
      })
    );
  }

  deleteProduit(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(error => {
        console.error(`Erreur lors de la suppression du produit ${id}`, error);
        return throwError(() => error);
      })
    );
  }
}
