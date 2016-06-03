import { Component } from 'angular2/core'
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router'
import {LoginPanelComponent} from '../login/loginPanel.component'
import {IsLoggedInDirective} from '../../shared/auth/isLoggedIn.directive'
import {IsNotLoggedInDirective} from '../../shared/auth/isNotLoggedIn.directive'
import {DashboardComponent} from '../dashboard/dashboard.component'
import {LoginComponent} from '../login/login.component'
import {RegistrationComponent} from '../login/registration.component'
import {HomeComponent} from '../home/home.component'
import {ProfileComponent} from '../profile/profile.component'

@Component({
    selector: 'app-component',
    templateUrl: '/Home/AppLayout',
    providers: [
        ROUTER_PROVIDERS
    ],
    directives: [
        ROUTER_DIRECTIVES,
        LoginPanelComponent,
        IsLoggedInDirective,
        IsNotLoggedInDirective,
    ]
})
@RouteConfig([
    {
        path: '/home/...',
        name: 'Home',
        component: HomeComponent,
        useAsDefault: true
    },
    {
        path: '/dashboard',
        name: 'Dashboard',
        component: DashboardComponent
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginComponent
    },
    {
        path: '/registration',
        name: 'Registration',
        component: RegistrationComponent
    },
    {
        path: '/profile/...',
        name: 'Profile',
        component: ProfileComponent
    },
])
export class AppComponent {
}