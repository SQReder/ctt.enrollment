import {Component, Input, Output, EventEmitter, OnInit} from "@angular/core"
import {CORE_DIRECTIVES} from "@angular/common"
import {ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated"
import {EnrolleeListModel} from "./enrollee.list.model";
import {EnrolleeViewComponent} from "./enrollee.view.component";
import {IGenericResult, IErrorResult, IGetEnrolleesResult} from "../../../../shared/responses/httpResults";
import {TrusteeService} from "../../../../shared/api/trustee.service";
import {Enrollee} from "../../../../shared/model/enrollee.class";
import {Notifier} from "../../../../shared/uikit/notify.service";
import {ModalService} from "../../../../shared/uikit/modal.service";

@Component({
    selector: "enroll-enrollee-list",
    templateUrl: "/Enrollee/ListLayout",
    directives: [
        CORE_DIRECTIVES,
        ROUTER_DIRECTIVES,
        EnrolleeViewComponent
    ]
})
export class EnrolleeListComponent implements OnInit {
    model: Enrollee[];

    constructor(
        private trusteeService: TrusteeService,
        private router: Router,
        private notifier: Notifier,
        private modal: ModalService
    ) {
    }

    ngOnInit() {
        this.load();
    }

    load() {
        this.trusteeService
            .getEnrollees()
            .subscribe((result: IGetEnrolleesResult) => {
                console.log(result);
                this.model = result.enrollees;
            });        
    }

    createNew() {
        this.router.navigate(["/Enrollee/Edit", {id: null}]);
    }

    onRequestEdit(id: string) {
        this.router.navigate(["/Enrollee/Edit", { id: id }]);
    }

    onRequestRemove(id: string) {
        const responceHandler = (result: IGenericResult | IErrorResult) => {
            if (result.succeeded) {
                this.notifier.success("Успешно удалено");
                this.load();
            } else {
                const errorResult = result as IErrorResult;

                this.notifier.error(errorResult.message);
                console.log(errorResult.message, errorResult);
            }
        };

        const confirmationHandler = (confirmed: boolean) => {
            if (confirmed) {
                this.trusteeService
                    .deleteEnrollee(id)
                    .subscribe(responceHandler);
            }
        };

        this.modal.confirm("Delete enrollee?")
            .subscribe(confirmationHandler);

    }
}