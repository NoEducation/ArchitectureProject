import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

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
    private route: ActivatedRoute)
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
            this.router.navigate([this.returnUrl ?? 'admin/dashboard']);
        },
        error: error => {
            debugger;
            alert(error.error)
            this.loading = false;
        }
    });
  }
}
