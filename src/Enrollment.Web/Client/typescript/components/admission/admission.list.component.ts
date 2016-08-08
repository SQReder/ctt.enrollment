import {Component, OnInit} from "@angular/core";
import {RouteConfig, ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";
import {AdmissionListModel} from "./admission.list.model";

@Component({
    selector: "enroll-admission-list",
    templateUrl: "/views/components/admission/list.html",
    providers: [
        AdmissionListModel
    ]
})
export class AdmissionListComponent implements OnInit {
    constructor(
        private router: Router,
        public model: AdmissionListModel
    ) {
    }

    ngOnInit() {
        this.model.init();
    }

    get haveAnyAdmissions() {
        return this.model.admissions != null &&
            this.model.admissions.length > 0;
    }

    createNew() {
        this.router.navigate(["/Admission/Edit"]);
    }
}