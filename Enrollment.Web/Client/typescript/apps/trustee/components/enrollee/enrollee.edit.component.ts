import {Component, Input, Output, EventEmitter, OnInit} from "@angular/core"
import {RouteParams, Router, ROUTER_DIRECTIVES} from "@angular/router-deprecated";
import {Observable} from "rxjs/Observable";

import {Enrollee} from "../../../../shared/model/enrollee.class";
import {TrusteeService} from "../../../../shared/api/trustee.service";
import * as HttpResults from "../../../../shared/responses/httpResults";
import {RelationTypeStringPipe} from "../../../../shared/pipes/relationType.pipe";
import {RelationTypeEnum} from "../../../../shared/model/relationType.enum";
import {GuidGeneratorService} from "../../../../shared/guidGenerator/guidGenerator.service";
import {RandomIdGenerator} from "../../../../shared/randomIdGenerator/randomIdGenerator.service";
import {Notifier} from "../../../../shared/uikit/notify.service";

@Component({
    selector: "enroll-enrollee-edit",
    templateUrl: "/Enrollee/EditLayout",
    directives: [
        ROUTER_DIRECTIVES
    ],
    pipes: [RelationTypeStringPipe]
})
export class EnrolleeEditComponent implements OnInit {
    newEnrollee: boolean;
    model = new Enrollee();
    trusteeAddress: string;
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
        private router: Router,
        private trusteeService: TrusteeService,
        private guidGenerator: GuidGeneratorService,
        private randomIdGenerator: RandomIdGenerator
    ) {
    }

    ngOnInit() {
        const id = this.routeParams.get("id");
        if (id !== null) {
            this.trusteeService.getEnrollee(id)
                .subscribe((result: HttpResults.IGetEnrolleeResult) => {
                    if (result.succeeded) {
                        let enrollee = result.enrollee;
                        if (enrollee == null)
                            enrollee = new Enrollee();

                        this.applyModel(enrollee);
                    } else {
                        console.error(result.message);
                    }
                });
        } else {
            this.applyModel(null);
        }

        this.trusteeService.getAddress()
            .subscribe((result: string) => {
                this.trusteeAddress = result;
            });
    }

    applyModel(model: Enrollee) {
        if (model != null) {
            this.model = model;
            this.newEnrollee = false;
        } else {
            this.model = new Enrollee;

            this.assignNewIdentity(this.model);

            this.newEnrollee = true;
        }

        this.initialized = true;
    }

    assignNewIdentity(model: Enrollee) {
        model.id = this.guidGenerator.create();
        model.alternateId = this.randomIdGenerator.create();
    }

    doSave() {
        let observable: Observable<HttpResults.IGuidResult | HttpResults.IErrorResult>;

        if (this.newEnrollee) {
            observable = this.trusteeService.createEnrollee(this.model);
        } else {
            observable = this.trusteeService.updateEnrollee(this.model);
        }

        observable.subscribe((result: HttpResults.IGuidResult | HttpResults.IErrorResult) => {
            console.log(result);
            if (result.succeeded) {
                // const guidResult = result as HttpResults.IGuidResult;
                this.router.navigateByUrl(`/enrollee`);
            } else {
                const errorResult = result as HttpResults.IErrorResult;
                if (errorResult.errorType === "EntityAlreadyExistsException") {
                    this.assignNewIdentity(this.model);
                    this.doSave();
                } else {
                    console.error(result.message, result);
                }
            }
        });
    }

    addressSameAsParentChanged(value: any) {
        if (value === true) {
            this.model.address = this.trusteeAddress;
        }
    }
}