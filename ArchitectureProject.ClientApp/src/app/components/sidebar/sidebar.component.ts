import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { RouteInfo } from 'src/app/core/models/layout/route-info.model';
import { AuthService } from 'src/app/core/services/auth.service';

export const ROUTES: RouteInfo[] = [
    { path: 'admin/dashboard', title: 'Ekran główny',  icon: 'ni-tv-2 text-primary', class: '', requiredLogin :  true},
    { path: 'admin/user-profile', title: 'Profil',  icon:'ni-single-02 text-yellow', class: '', requiredLogin :  true },
    { path: 'auth/login', title: 'Logowanie',  icon:'ni-key-25 text-info', class: '' , requiredLogin :  false},
    { path: 'auth/register', title: 'Rejstracja',  icon:'ni-circle-08 text-pink', class: '' , requiredLogin :  false}
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit, OnDestroy {

  public menuItems: any[];
  public isCollapsed = true;
  public isLogged = false;

  constructor(private readonly router: Router,
    private readonly authService : AuthService) { }

    ngOnInit() {
      this.menuItems = ROUTES;
      this.authService.userLogged.subscribe(
       isLogged => {
        this.isLogged = isLogged;
         this.menuItems = ROUTES.filter(menuItem => {
           if(menuItem.requiredLogin){
             return isLogged && isLogged == true;
            }
            else{
              return !isLogged;
            }
          });
      });

      this.router.events.subscribe((event) => {
        this.isCollapsed = true;
      });
    }

    ngOnDestroy(): void {
      this.authService.userLogged.unsubscribe();
    }

    logout(){
      this.authService.logout();
    }
}
