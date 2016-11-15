import {Component} from "@angular/core";
import {RouteConfig, ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";
import {TrusteeViewComponent} from "./trustee.view.component";
import {TrusteeEditComponent} from "./trustee.edit.component";

@Component({
    selector: "enroll-trustee-component",
    template: "<router-outlet></router-outlet>",
    directives: [
        ROUTER_DIRECTIVES
    ]
})
@RouteConfig([
    {
        path: "view",
        component: TrusteeViewComponent,
        name: "View",
        useAsDefault: true
    },
    {
        path: "edit",
        component: TrusteeEditComponent,
        name: "Edit"
    }
])
export class TrusteeComponent {

}