import {Injectable, OnInit} from "@angular/core";

import {TrusteeService} from "../../shared/api/trustee.service";
import {Admission} from "../../shared/model/admission.class";

@Injectable()
export class AdmissionListModel {
    admissions: Admission[];

    constructor(
        private trusteeService: TrusteeService
    ) {        
    }

    init() {
        this.trusteeService
            .getCurrentTrusreeAdmissions()
            .subscribe((admissions: Admission[]) => {
                this.admissions = admissions;
            });
    }

    get isInitialized() {
        return this.admissions != null;
    }
}