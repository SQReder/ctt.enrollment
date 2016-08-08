import {Injectable, OnInit} from "@angular/core"
import {DeprecatedEnrolleeService as EnrolleeService} from "../../shared/enrollee/enrollee.service";
import {DeprecatedEnrollee as Enrollee} from "./enrollee.class";
import * as HttpResults from "../../shared/responses/httpResults";

@Injectable()
export class EnrolleeList implements OnInit {
    items: Enrollee[] = [];

    constructor(
        private enrolleeService: EnrolleeService
    ) {
    }

    ngOnInit() {
        this.enrolleeService
            .listEnrollee()
            .subscribe((result: HttpResults.IListEnrolleeResult) => {
                if (result.succeeded) {
                    this.items = result.enrollees;
                }
            });
    }

    getList() {
        return this.items;
    }

    onRequestRemove(id: string) {
        console.log("remove requested", id);
    }
}