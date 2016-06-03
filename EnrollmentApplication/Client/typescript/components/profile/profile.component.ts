import { Component } from 'angular2/core'
import { RouteConfig, ROUTER_DIRECTIVES } from 'angular2/router'
import {ProfileViewComponent} from './profileView.component'

@Component({
    selector: 'enroll-profile',
    templateUrl: '/Profile/Index',
    directives: [
        ROUTER_DIRECTIVES
    ]
})
@RouteConfig([
    {
        path: 'view',
        component: ProfileViewComponent,
        name: 'View',
        useAsDefault: true
    }
])
export class ProfileComponent {
}