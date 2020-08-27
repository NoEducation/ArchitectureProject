import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { Router } from '@angular/router';
import { RegisterUser } from 'src/app/core/models/register/register-user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  user : RegisterUser = new RegisterUser();

  constructor(
    private readonly authService : AuthService,
    private readonly notificationService : NotificationService,
    private readonly router : Router
  ) { }

  ngOnInit() {
  }

  registerUser() : void {
    this.authService.register(this.user)
      .subscribe( () => {
          this.notificationService.successMessage('Utworzono nowego użytkownika, możesz się zalogować', "Sukces");
          this.router.navigateByUrl('auth/login');
      }, error => this.notificationService.errorMessage(error.error));
  }

}
