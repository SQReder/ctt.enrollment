import {Component, Input, Output, EventEmitter, OnInit} from "@angular/core"
import {Enrollee, Address } from "./enrollee.class";
import {RelationTypeStringPipe, RelationTypeEnum} from "./relationType.enum"

import {EnrolleeService} from "../../shared/enrollee/enrollee.service";
import {RouteParams, ROUTER_DIRECTIVES} from "@angular/router-deprecated";
import {TrusteeService} from "../../shared/trustee/trustee.service";
import {IGuidResult, ITrusteeAddressResult, IGetEnrolleeResult} from "../../shared/responses/httpResults"

@Component({
    selector: "enroll-enrollee-edit",
    templateUrl: "/Enrollee/EditLayout",
    directives: [
        ROUTER_DIRECTIVES
    ],
    pipes: [RelationTypeStringPipe]
})
export class EnrolleeEditComponent implements OnInit {
    model = new Enrollee();
    trusteeAddress = new Address();
    submitted = false;

    initialized: boolean;

    relations: RelationTypeEnum[] = [
        RelationTypeEnum.Child,
        RelationTypeEnum.Grandchild,
        RelationTypeEnum.Ward
    ];

    onSubmit() { this.submitted = true; }
    // TODO: Remove this when we're done
    get diagnostic() { return JSON.stringify(this.model) + JSON.stringify(this.trusteeAddress); }

    constructor(
        private routeParams: RouteParams,
        private service: EnrolleeService,
        private trusteeService: TrusteeService
    ) {
    }

    ngOnInit() {
        const id = this.routeParams.get("id");
        if (id !== undefined) {
            this.service.getEnrollee(id)
                .subscribe((result: IGetEnrolleeResult) => {
                    let enrollee = result.enrollee;
                    if (enrollee == null)
                        enrollee = new Enrollee();

                    this.applyModel(enrollee);
                });
        } else {
            this.applyModel(new Enrollee());
        }

        this.trusteeService.getCurrentTrusteeAddress()
            .subscribe((result: ITrusteeAddressResult) => {
                this.trusteeAddress = result.address;
            });
    }

    applyModel(model: Enrollee) {
        this.model = model;
        this.initialized = true;
    }

    doSave() {
        this.service.saveEnrollee(this.model)
            .subscribe((result: IGuidResult) => {
                console.log(result);
            });
    }
}