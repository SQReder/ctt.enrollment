"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var login_component_1 = require("./login.component");
var registration_component_1 = require("./registration.component");
var isLoggedIn_directive_1 = require("../../shared/auth/isLoggedIn.directive");
var isNotLoggedIn_directive_1 = require("../../shared/auth/isNotLoggedIn.directive");
var LoginPanelComponent = (function () {
    function LoginPanelComponent(authService) {
        this.authService = authService;
    }
    LoginPanelComponent.prototype.logout = function () {
        this.authService.logout();
    };
    LoginPanelComponent = __decorate([
        core_1.Component({
            selector: "[loginPanel]",
            templateUrl: "./Account/LoginPanel",
            directives: [router_deprecated_1.ROUTER_DIRECTIVES, isLoggedIn_directive_1.IsLoggedInDirective, isNotLoggedIn_directive_1.IsNotLoggedInDirective]
        }),
        router_deprecated_1.RouteConfig([
            {
                path: "/login",
                component: login_component_1.LoginComponent,
                name: "Login"
            },
            {
                path: "/registration",
                component: registration_component_1.RegistrationComponent,
                name: "Registration"
            },
        ])
    ], LoginPanelComponent);
    return LoginPanelComponent;
}());
exports.LoginPanelComponent = LoginPanelComponent;
