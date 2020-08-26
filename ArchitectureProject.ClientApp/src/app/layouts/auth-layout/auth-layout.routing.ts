import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ROUTE } from 'src/app/core/enums/route.enum';

export const AuthLayoutRoutes: Routes = [
    { path: ROUTE.Login , component: LoginComponent },
    { path: ROUTE.Register,  component: RegisterComponent }
];
