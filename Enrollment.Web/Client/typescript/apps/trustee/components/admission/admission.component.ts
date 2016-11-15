import {Component} from "@angular/core";
import {RouteConfig, ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";

import {AdmissionListComponent} from "./admission.list.component";
import {AdmissionEditComponent} from "./admission.edit.component";
import {AdmissionViewComponent} from "./admission.view.component";

@Component({
    selector: "enroll-admission",
    templateUrl: "/views/components/admission/root.html",
    directives: [
        ROUTER_DIRECTIVES
    ]
})
@RouteConfig([
    {
        path: "/list",
        component: AdmissionListComponent,
        name: "List",
        useAsDefault: true
    },
    {
        path: "/edit",
        component: AdmissionEditComponent,
        name: "Edit"
    },
    {
        path:  "/view/:id",
        component: AdmissionViewComponent,
        name: "View"
    }
])
export class AdmissionComponent {
}