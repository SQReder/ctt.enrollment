import {Component, OnInit} from "@angular/core"
import {RouteParams, Router, ROUTER_DIRECTIVES} from "@angular/router-deprecated";

import {IGetAdmissionResult, IErrorResult} from "../../../../shared/responses/httpResults";

import {TrusteeService} from "../../../../shared/api/trustee.service";
import {Admission} from "../../../../shared/model/admission.class";

import {AdmissionViewModel} from "./admission.view.model";

@Component({
    selector: "enroll-admission-view",
    providers: [
        AdmissionViewModel
    ]
})
export class AdmissionViewComponent implements OnInit {
    constructor(
        private routeParams: RouteParams,
        private model: AdmissionViewModel
    ) {
    }

    ngOnInit() {
        const id = this.routeParams.get("id");
        this.model.load(id);
    }
}