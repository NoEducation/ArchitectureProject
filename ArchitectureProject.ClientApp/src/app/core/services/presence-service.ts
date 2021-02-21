import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { ToastrService } from "ngx-toastr";
import { environment } from "src/environments/environment";
import { LocalStorageService } from "./local-storage.service";

@Injectable( {providedIn: 'root'})
export class PresenceService {
  url : string = environment.hubsUrl;

  private defaultHubName : string = "default-hub";
  private hubConnection : HubConnection;

  constructor(private readonly toasterService: ToastrService,
    private readonly localStorage : LocalStorageService)
  {}

  startHubConnection() : void{
    const accessToken = this.localStorage.getAccessToken();

    if(!accessToken)
      return;

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.url}${this.defaultHubName}`,{
          accessTokenFactory : () => accessToken
        })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .catch(error => console.log(error));

    this.hubConnection.on('UserIsLogged', (args) => {
      const message = args ? args : 'Some user logged';
      this.toasterService.success(message, 'Connected');
    });

    this.hubConnection.on('UserIsDisconnected', (args) => {
      const message = args ? args : 'Some user disconnected';
      this.toasterService.warning(message, 'Disconnected')
    })
  }

  stopHubConnection() : void{
    this.hubConnection
      .stop()
      .catch(error => console.log(error))
  }
}
