import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { AuthService } from "./auth.service";
import { LocalStorageService } from "./local-storage.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor{

  constructor(private readonly authService : AuthService,
    private readonly localStorage : LocalStorageService){
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if(this.authService.isLogged){

      const headers = new HttpHeaders();
      headers.append("Authorization" , `Bearer ${this.localStorage.getAccessToken()}`)

      req = req.clone({
        headers : headers
      })
    }

    return next.handle(req);
  }

}
