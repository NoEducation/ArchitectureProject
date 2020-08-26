import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { LocalStorageService } from "./local-storage.service";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { map } from 'rxjs/operators';
import { ROUTE } from "../enums/route.enum";
import { Router } from "@angular/router";

@Injectable({ providedIn : 'root'})
export class AuthService {

    private userLogged: BehaviorSubject<boolean>;

    constructor(private readonly httpClient : HttpClient,
      private readonly localStorage : LocalStorageService,
      private readonly router : Router)
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
                  return resp;
              }));
    }

    logout(){
      debugger;
      this.localStorage.removeAccessToken();
      this.userLogged.next(null);
      this.router.navigate([ROUTE.Login]);
    }
}
