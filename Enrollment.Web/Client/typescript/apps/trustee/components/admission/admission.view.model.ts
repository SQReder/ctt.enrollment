import {Injectable, OnInit} from "@angular/core";
import {RouteParams, Router, ROUTER_DIRECTIVES} from "@angular/router-deprecated";

import {IGetAdmissionResult, IErrorResult, IGenericResult} from "../../../../shared/responses/httpResults";

import {TrusteeService} from "../../../../shared/api/trustee.service";
import {Admission} from "../../../../shared/model/admission.class";

@Injectable()
export class AdmissionViewModel {
    model: Admission;

    constructor(
        private trusteeService: TrusteeService,
        private router: Router
    ) {        
    }

    load(id: string) {
        if (id != null) {
            this.trusteeService
                .getAdmission(id)
                .subscribe((result: IGetAdmissionResult | IErrorResult) => {
                    if (result.succeeded) {
                        const admissionResult = result as IGetAdmissionResult;
                        this.applyModel(admissionResult.admission);
                    } else {
                        const errorResult = result as IErrorResult;
                        console.error(errorResult.message, errorResult);
                    }
                });
        } else {
            this.router.navigateByUrl("/admissions");
        }
    }

    private applyModel(admission: Admission) {
        this.model = admission;
    }
}