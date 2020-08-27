import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  email : string = '';
  password : string = '';
  returnUrl : string;
  loading : boolean = false;

  constructor(private readonly authService : AuthService,
    private readonly router : Router,
    private readonly route: ActivatedRoute,
    private readonly notificationService : NotificationService )
  {}

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || null;
  }

  ngOnDestroy() {
  }

  login(){
    this.authService.login(this.email,this.password)
      .pipe(first())
      .subscribe({
        next: () => {
            this.notificationService.successMessage("Witaj", "Poprawnie zalogowano")
            this.router.navigate([this.returnUrl ?? 'admin/dashboard']);
        },
        error: error => {
            this.notificationService.errorMessage(error.error);
        }
    });
  }
}
