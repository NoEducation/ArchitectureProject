import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class SetRequestInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
      let headers : HttpHeaders = new HttpHeaders();
      headers.append('Content-Type', 'application/json');

      const request = req.clone({
        headers : headers,
        withCredentials : true
      });

      return next.handle(request);
    }
}
