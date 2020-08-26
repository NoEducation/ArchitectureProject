import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import {ROUTE} from './core/enums/route.enum'
import { AuthGuard } from './core/services/auth-guard.service';

const routes: Routes =[
  {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full',
  },
  {
    path: ROUTE.Admin,
    loadChildren: './layouts/admin-layout/admin-layout.module#AdminLayoutModule',
    canActivate : [AuthGuard]
  },
  {
    path: ROUTE.Auth,
    loadChildren: './layouts/auth-layout/auth-layout.module#AuthLayoutModule'
  },
  {
    path: '**',
    redirectTo: ROUTE.Dashboard
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
