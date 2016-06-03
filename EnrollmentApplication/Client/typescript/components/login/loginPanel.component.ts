'use strict';
import {Component, provide} from 'angular2/core';
import {Router, RouteConfig, RouteParams, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';

import {AuthService} from '../../shared/auth/auth.service'
import {LoginComponent} from './login.component'
import {RegistrationComponent} from './registration.component';

import { IsLoggedInDirective } from '../../shared/auth/isLoggedIn.directive'
import { IsNotLoggedInDirective } from '../../shared/auth/isNotLoggedIn.directive'

@Component({
    selector: '[loginPanel]',
    templateUrl: './Account/LoginPanel',
    directives: [ROUTER_DIRECTIVES, IsLoggedInDirective, IsNotLoggedInDirective]
})

@RouteConfig([
    {
        path: '/login',
        component: LoginComponent,
        name: 'Login'
    },
    {
        path: '/registration',
        component: RegistrationComponent,
        name: 'Registration'
    },
])
export class LoginPanelComponent {

    constructor(private authService: AuthService) {
    }

    logout() {
        this.authService.logout();
    }
}