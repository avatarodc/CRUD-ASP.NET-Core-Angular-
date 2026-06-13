import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProduitListComponent } from './components/produit-list/produit-list.component';
import { ProduitFormComponent } from './components/produit-form/produit-form.component';
import { authGuard, guestGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/produits', pathMatch: 'full' },

  // Routes publiques (invités seulement)
  {
    path: 'auth',
    canActivate: [guestGuard],
    children: [
      {
        path: 'login',
        loadComponent: () =>
          import('./features/auth/login/login.component').then(m => m.LoginComponent)
      },
      {
        path: 'register',
        loadComponent: () =>
          import('./features/auth/register/register.component').then(m => m.RegisterComponent)
      },
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  },

  // Routes protégées
  {
    path: 'produits',
    canActivate: [authGuard],
    component: ProduitListComponent
  },
  {
    path: 'produits/new',
    canActivate: [authGuard],
    component: ProduitFormComponent
  },
  {
    path: 'produits/edit/:id',
    canActivate: [authGuard],
    component: ProduitFormComponent
  },

  { path: '**', redirectTo: '/produits' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
