import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ROUTE } from 'src/app/core/enums/route.enum';
import { AuthGuard } from 'src/app/core/services/auth-guard.service';

export const AdminLayoutRoutes: Routes = [
    { path: ROUTE.Dashboard,   component: DashboardComponent ,    canActivate : [AuthGuard]},
    { path: ROUTE.UserProfile, component: UserProfileComponent ,   canActivate : [AuthGuard]},
];
