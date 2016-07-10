"use strict";
import {Component, provide} from "@angular/core";
import {NgForm} from "@angular/forms";
import {Router, RouteParams, ROUTER_DIRECTIVES, CanActivate} from "@angular/router-deprecated";
import {AuthService} from "../../shared/auth/auth.service"
import {LoginModel} from "../../shared/auth/LoginModel";

@Component({
    selector: "enroll-login",
    templateUrl: "./Account/Login"
})
export class LoginComponent {
    model: LoginModel;

    constructor(
        private params: RouteParams,
        private authService: AuthService
    ) {
        this.model = new LoginModel();
        this.model.username = "71235678901";
        this.model.password = "abcd1234!";
        this.model.returnUrl = params.get("returnUrl");
    }

    login() {
        this.authService.login(this.model);
    }
}