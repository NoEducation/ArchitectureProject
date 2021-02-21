import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { LocalStorageService } from './core/services/local-storage.service';
import { AuthService } from './core/services/auth.service';
import { AuthGuard } from './core/services/auth-guard.service';
import { JwtInterceptor } from './core/services/jwt.interceptor';
import { SetRequestInterceptor } from './core/services/set-request.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { NotificationService } from './core/services/notification.service';
import { PresenceService } from './core/services/presence-service';
@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
    }),
  ],
  declarations: [
    AppComponent,
  ],
  providers: [
    LocalStorageService,
    AuthService,
    AuthGuard,
    NotificationService,
    PresenceService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: SetRequestInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
