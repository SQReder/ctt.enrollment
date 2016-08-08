﻿import {Component, Input, Output, EventEmitter, OnInit} from "@angular/core"
import {CORE_DIRECTIVES} from "@angular/common"
import {ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated"
import {DeprecatedEnrolleeService as EnrolleeService} from "../../shared/enrollee/enrollee.service"
import {EnrolleeList} from "./enrolleeList.class";
import {DeprecatedEnrollee as Enrollee} from "./enrollee.class";
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
        this.load();
    }

    load() {
        this.enrolleeService
            .listEnrollee()
            .subscribe((result: HttpResults.IListEnrolleeResult) => {
                console.log(result);
                this.model = result.enrollees;
            });
        
    }

    createNew() {
        this.router.navigate(["/Enrollee/Edit", {id: ""}]);
    }

    onRequestEdit(id: string) {
        this.router.navigate(["/Enrollee/Edit", { id: id }]);
    }

    onRequestRemove(id: string) {
        const result = confirm("Delete enrollee?");

        if (result) {
            this.enrolleeService
                .deleteEnrollee(id)
                .subscribe((result: HttpResults.IGenericResult) => {
                    console.log(result);
                    this.load();
                });
        }
    }
}