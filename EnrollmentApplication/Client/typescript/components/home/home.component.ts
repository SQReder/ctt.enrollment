import {Component} from '@angular/core'
import {RouteConfig, RouterOutlet} from '@angular/router-deprecated'
import {WelcomeComponent} from './welcome.component';

@Component({
    selector: 'enroll-home',
    template: '<router-outlet></router-outlet>',
    directives: [
        RouterOutlet
    ]
})
@RouteConfig([
    {
        path: '/',
        component: WelcomeComponent,
        name: 'Welcome',
        useAsDefault: true
    }
])
export class HomeComponent {
}                            