import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router-deprecated";

import {AdmissionModel} from "./admission.edit.model";
import {UnityService} from "../../shared/api/unity.service";
import {TrusteeService} from "../../shared/api/trustee.service";

@Component({
    selector: "enroll-admission-edit",
    templateUrl: "/views/components/admission/edit.html",
    providers: [
        AdmissionModel
    ]
})
export class AdmissionEditComponent implements OnInit {
    constructor(
        public model: AdmissionModel,
        private router: Router
    ) {
    }

    ngOnInit() {
        this.model.init();
    }

    diagnostic() {
        return JSON.stringify({
            group: this.model.selectedGroup
        });
    }
}