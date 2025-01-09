import { Routes } from "@angular/router";
import { InvalidPageComponent } from "./components/auth/invalid-page/invalid-page.component";
import { LoggedOutComponent } from "./components/auth/logged-out/logged-out.component";
import { LoginComponent } from "./components/auth/login/login.component";
import { RegisterComponent } from "./components/auth/login/register/register.component";
import { ResetPasswordComponent } from "./components/auth/login/reset-password/reset-password.component";
import { NoAccessComponent } from "./components/auth/no-access/no-access.component";

export const coreRoutes: Routes = [
    { 
      path: '', 
      redirectTo: 'login', 
      pathMatch: 'full'
    },
    { 
      path: 'invalid-page', 
      component: InvalidPageComponent 
    },
    { 
      path: 'logged-out', 
      component: LoggedOutComponent 
    },
    { 
      path: 'login', 
      component: LoginComponent 
    },
    { 
      path: 'login/register', 
      component: RegisterComponent 
    },
    { 
      path: 'login/reset-password', 
      component: ResetPasswordComponent
    },
    { 
      path: 'no-access', 
      component: NoAccessComponent 
    }
  ];