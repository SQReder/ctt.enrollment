import {Component} from "@angular/core"
import {CanActivate} from "@angular/router-deprecated"
import {isLoggedIn} from "../../shared/auth/isLoggedIn.method";


@Component({
    selector: "enroll-dashboard",
    templateUrl: "/Dashboard/Layout",
})
@CanActivate(isLoggedIn)
export class DashboardComponent {
    constructor(
    ) {
    }
}