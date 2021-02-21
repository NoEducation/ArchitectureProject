import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { LocalStorageService } from "./local-storage.service";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { map } from 'rxjs/operators';
import { ROUTE } from "../enums/route.enum";
import { Router } from "@angular/router";
import { RegisterUser } from "../models/register/register-user.model";
import { NotificationService } from "./notification.service";
import { PresenceService } from "./presence-service";

@Injectable({ providedIn : 'root'})
export class AuthService {

    public userLogged: BehaviorSubject<boolean>;

    constructor(private readonly httpClient : HttpClient,
      private readonly localStorage : LocalStorageService,
      private readonly notificationService : NotificationService,
      private readonly router : Router,
      private readonly presenceService : PresenceService)
    {
        this.userLogged = new BehaviorSubject<boolean>(null);
    }

    isLogged() : boolean{
      return !!this.userLogged.value && this.userLogged.value;
    }

    login(email : string, password : string) : Observable<string> {
        const request  = {
          email : email,
          password : password
        }

        return this.httpClient.post<string>(`${environment.apiUrl}/Account/Login`, request)
                .pipe(map(resp => {
                  this.localStorage.setAccessToken(resp)
                  this.userLogged.next(true);
                  this.presenceService.startHubConnection();
                  return resp;
              }));
    }

    logout() : void{
      this.localStorage.removeAccessToken();
      this.userLogged.next(null);
      this.presenceService.stopHubConnection();
      this.router.navigate([ROUTE.Login]);
      this.notificationService.successMessage("Wylogowano użytkownika")
    }

    register(user : RegisterUser) : Observable<any> {
      return this.httpClient.post(`${environment.apiUrl}/Account/Register`, user);
   }
}
