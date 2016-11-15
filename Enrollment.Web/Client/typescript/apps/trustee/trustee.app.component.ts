"use strict";
import {Component} from "@angular/core";
import {RouteConfig, ROUTER_DIRECTIVES} from "@angular/router-deprecated";
import {LoginPanelComponent} from "./components/login/loginPanel.component";
import {IsLoggedInDirective} from "../../shared/auth/isLoggedIn.directive";
import {IsNotLoggedInDirective} from "../../shared/auth/isNotLoggedIn.directive";

import {LoginComponent} from "./components/login/login.component"
import {RegistrationComponent} from "./components/login/registration.component"
import {HomeComponent} from "./components/home/home.component"
import {TrusteeComponent} from "./components/trustee/trustee.component"
import {EnrolleesComponent} from "./components/enrollee/enrollee.component";
import {AdmissionComponent} from "./components/admission/admission.component";



@Component({
    selector: "trustee-app-component",
    templateUrl: "/views/app/trustee/app.component.html",
    providers: [],
    directives: [
        ROUTER_DIRECTIVES,
        LoginPanelComponent,
        IsLoggedInDirective,
        IsNotLoggedInDirective,
    ]
})
@RouteConfig([
    {
        path: "/home/...",
        name: "Home",
        component: HomeComponent,
        useAsDefault: true
    },
    {
        path: "/login",
        name: "Login",
        component: LoginComponent
    },
    {
        path: "/registration",
        name: "Registration",
        component: RegistrationComponent
    },
    {
        path: "/profile/...",
        name: "Trustee",
        component: TrusteeComponent
    }, 
    {
        path: "/enrollee/...",
        name: "Enrollee",
        component: EnrolleesComponent
    },
    {
        path: "/admission/...",
        name: "Admission",
        component: AdmissionComponent
    }
])
export class TrusteeAppComponent {
}