import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: 'admin/dashboard', title: 'Ekran główny',  icon: 'ni-tv-2 text-primary', class: '' },
    { path: 'admin/user-profile', title: 'Profil',  icon:'ni-single-02 text-yellow', class: '' },
    { path: 'auth/login', title: 'Logowanie',  icon:'ni-key-25 text-info', class: '' },
    { path: 'auth/register', title: 'Rejstracja',  icon:'ni-circle-08 text-pink', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private router: Router) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
}
