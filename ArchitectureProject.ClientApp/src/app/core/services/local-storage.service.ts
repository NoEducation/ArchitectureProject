import { Injectable } from "@angular/core";

@Injectable( {providedIn: 'root'})
export class LocalStorageService {

    private accessTokenKey = 'access-token';

    setAccessToken(value : string){
        localStorage.setItem(this.accessTokenKey, value);
    }

    removeAccessToken(){
        localStorage.removeItem(this.accessTokenKey)
    }
}

