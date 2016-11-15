import {Injectable, OnInit, EventEmitter} from "@angular/core";
import {Observable} from "rxjs/Observable";

import {IGenericResult, IErrorResult, IGetAdmissionsResult} from "../../../../shared/responses/httpResults";

import {TrusteeService} from "../../../../shared/api/trustee.service";
import {Admission} from "../../../../shared/model/admission.class";
import {Notifier} from "../../../../shared/uikit/notify.service";
import {ModalService} from "../../../../shared/uikit/modal.service";

@Injectable()
export class AdmissionListModel {
    admissions: Admission[];

    admissionRemoved = new EventEmitter<string>();

    constructor(
        private trusteeService: TrusteeService,
        private notifier: Notifier,
        private modalService: ModalService
    ) {
    }

    load() {
        this.admissions = undefined;

        this.trusteeService
            .getAdmissions()
            .subscribe((result: IGetAdmissionsResult | IErrorResult) => {
                if (result.succeeded) {
                    const admissionsResult = result as IGetAdmissionsResult;
                    this.admissions = admissionsResult.admissions;
                } else {
                    const errorResult = result as IErrorResult;
                    this.notifier.error(errorResult.message);
                    console.log(errorResult.message, errorResult);
                }
            });
    }

    get isInitialized() {
        return this.admissions != null;
    }

    remove(id: string) {
        if (id == null)
            throw new Error("argument 'id' is null");

        this.modalService.confirm("Удалить заявление?")
            .subscribe((confirmed: boolean) => {
                if (confirmed) {
                    this.trusteeService
                        .deleteAdmission(id)
                        .subscribe((result: IGenericResult | IErrorResult) => {
                            if (result.succeeded) {
                                this.admissionRemoved.emit(null);
                                this.notifier.success("Admission removed");
                            } else {
                                const errorResult = result as IErrorResult;
                                this.notifier.error(errorResult.message);
                                console.error(errorResult.message, errorResult);
                            }
                        });
                }
            });
    }

    download(id: string) {
        this.trusteeService.downloadAdmission(id)
            .subscribe(() => {
                
            });
    }
}