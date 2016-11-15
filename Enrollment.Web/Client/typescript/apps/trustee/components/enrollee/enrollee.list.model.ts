import {Injectable, OnInit} from "@angular/core"

import {Enrollee} from "../../../../shared/model/enrollee.class";
import {TrusteeService} from "../../../../shared/api/trustee.service";
import * as HttpResults from "../../../../shared/responses/httpResults";

@Injectable()
export class EnrolleeListModel implements OnInit {
    items: Enrollee[] = [];

    constructor(
        private trusteeService: TrusteeService
    ) {
    }

    ngOnInit() {
        this.trusteeService.getEnrollees()
            .subscribe((result: HttpResults.IGetEnrolleesResult) => {
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