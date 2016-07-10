"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var loginPanel_component_1 = require("../login/loginPanel.component");
var isLoggedIn_directive_1 = require("../../shared/auth/isLoggedIn.directive");
var isNotLoggedIn_directive_1 = require("../../shared/auth/isNotLoggedIn.directive");
var dashboard_component_1 = require("../dashboard/dashboard.component");
var login_component_1 = require("../login/login.component");
var registration_component_1 = require("../login/registration.component");
var home_component_1 = require("../home/home.component");
var profile_component_1 = require("../profile/profile.component");
var enrollees_component_1 = require("../enrollee/enrollees.component");
var AppComponent = (function () {
    function AppComponent() {
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: "app-component",
            templateUrl: "/Home/AppLayout",
            providers: [],
            directives: [
                router_deprecated_1.ROUTER_DIRECTIVES,
                loginPanel_component_1.LoginPanelComponent,
                isLoggedIn_directive_1.IsLoggedInDirective,
                isNotLoggedIn_directive_1.IsNotLoggedInDirective,
            ]
        }),
        router_deprecated_1.RouteConfig([
            {
                path: "/home/...",
                name: "Home",
                component: home_component_1.HomeComponent,
                useAsDefault: true
            },
            {
                path: "/dashboard",
                name: "Dashboard",
                component: dashboard_component_1.DashboardComponent
            },
            {
                path: "/login",
                name: "Login",
                component: login_component_1.LoginComponent
            },
            {
                path: "/registration",
                name: "Registration",
                component: registration_component_1.RegistrationComponent
            },
            {
                path: "/profile/...",
                name: "Profile",
                component: profile_component_1.ProfileComponent
            },
            {
                path: "/enrollee/...",
                name: "Enrollee",
                component: enrollees_component_1.EnrolleesComponent
            }
        ])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
