import { Injectable } from "@angular/core";
import { ToastrService, IndividualConfig } from "ngx-toastr";

@Injectable()
export class NotificationService {

  constructor(private toasterService: ToastrService)
  {}

  successMessage(message : string, title : string = null, options : Partial<IndividualConfig> | any = null) : void {
    this.toasterService.success(message, title, options)
  }

  errorMessage(message : string , title : string = null, options : Partial<IndividualConfig> | any = null){
    this.toasterService.error(message, title , options)
  }

}
