'use strict';
import {Component, provide} from '@angular/core';
import {FormBuilder, ControlGroup, Control, Validators} from '@angular/common';
import {Router, RouteParams, ROUTER_DIRECTIVES, ROUTER_PROVIDERS, CanActivate} from '@angular/router-deprecated';
import {AuthService} from '../../shared/auth/auth.service'

@Component({
    selector: 'enroll-login',
    templateUrl: './Account/Login',
})
export class LoginComponent {

    loginForm: ControlGroup;

    username: Control;
    password: Control;
    returnUrl: string;
    

    constructor(
        private params: RouteParams,
        private authService: AuthService,
        private formBuilder: FormBuilder
    ) {
        this.username = new Control(
            '71235678901',
            Validators.required
            //Validators.compose([Validators.required, Validators.pattern("[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$")])
        );
        this.password = new Control(
            'abcd1234!',
            Validators.required
        );
        this.returnUrl = params.get('returnUrl');

        this.loginForm = formBuilder.group({
            username: this.username,
            password: this.password
        });
    }

    login() {
        this.authService.login(this.username.value, this.password.value, this.returnUrl);
    }
}