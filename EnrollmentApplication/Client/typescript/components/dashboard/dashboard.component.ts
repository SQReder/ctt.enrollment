import {Component} from 'angular2/core'
import {CanActivate} from 'angular2/router'
import {isLoggedIn} from "../../shared/auth/isLoggedIn.method";


@Component({
    selector: 'enroll-dashboard',
    templateUrl: '/Dashboard/Layout',
})
@CanActivate(isLoggedIn)
export class DashboardComponent {
    constructor(
    ) {
    }
}