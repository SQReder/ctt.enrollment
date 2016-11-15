import {ModuleWithProviders} from "@angular/core";
import {Routes, RouterModule} from "@angular/router";
import {DashboardComponent} from "./components/dashboard/dashboard.component";
import {LoginComponent} from "./components/login/login-component";
import {RegistrationComponent} from "./components/registration/registration-component";
import {ProfileComponent} from "./components/profile/profile-component";
import {AdmissionComponent} from "./components/admission/root";

const appRoutes: Routes = [
  {
    path: "",
    redirectTo: "/dashboard",
    pathMatch: "full"
  },
  {
    path: "dashboard",
    component: DashboardComponent
  },
  {
    path: "login",
    component: LoginComponent
  }, {
    path: "registration",
    component: RegistrationComponent
  }, {
    path: "profile",
    component: ProfileComponent
  }, {
    path: "admission",
    component: AdmissionComponent
  }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
