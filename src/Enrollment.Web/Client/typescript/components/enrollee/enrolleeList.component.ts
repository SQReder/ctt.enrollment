import {Component, Input, Output, EventEmitter, OnInit} from "@angular/core"
import {CORE_DIRECTIVES} from "@angular/common"
import {ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated"
import {EnrolleeService} from "../../shared/enrollee/enrollee.service"
import {EnrolleeList} from "./enrolleeList.class";
import {Enrollee} from "./enrollee.class";
import {EnrolleeViewComponent} from "./enrolleeView.component";
import * as HttpResults from "../../shared/responses/httpResults";

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
        private enrolleeService: EnrolleeService,
        private router: Router
    ) {
    }

    ngOnInit() {
        this.enrolleeService
            .listEnrollee()
            .subscribe((result: HttpResults.IListEnrolleeResult) => {
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
}