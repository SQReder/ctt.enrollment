import {Component, Input, Output, EventEmitter, OnInit} from '@angular/core'
import {NgForm, FormBuilder, Control, Validators, ControlGroup} from '@angular/common'
import {Enrollee} from './enrollee.class';
import {RelationTypeStringPipe, RelationTypeEnum} from './relationType.enum'

import {EnrolleeService} from "../../shared/enrollee/enrollee.service";
import {RouteParams, ROUTER_DIRECTIVES} from '@angular/router-deprecated';
import {RadioControlValueAccessor} from '../../shared/directives/radioControlValueAccessor.directive';

@Component({
    selector: 'enroll-enrollee-edit',
    templateUrl: '/Enrollee/EditLayout',
    directives: [
        ROUTER_DIRECTIVES,
        RadioControlValueAccessor
    ],
    pipes: [RelationTypeStringPipe]
})
export class EnrolleeEditComponent implements OnInit {
    model = new Enrollee();
    submitted = false;

    initialized: boolean;

    relations: RelationTypeEnum[] = [
        RelationTypeEnum.Child,
        RelationTypeEnum.Grandchild,
        RelationTypeEnum.Ward
    ];

    onSubmit() { this.submitted = true; }
    // TODO: Remove this when we're done
    get diagnostic() { return JSON.stringify(this.model); }

    constructor(
        private routeParams: RouteParams,
        private service: EnrolleeService
    ) {
    }

    ngOnInit() {
        const id = this.routeParams.get('id');
        if (id !== undefined) {
            this.service.getEnrollee(id)
                .subscribe(result => {
                    let enrollee = result.enrollee;
                    if (enrollee == null)
                        enrollee = new Enrollee();

                    this.applyModel(enrollee);
                });
        } else {
            this.applyModel(new Enrollee());
        }
    }

    applyModel(model: Enrollee) {
        this.model = model;
        this.initialized = true;
    }

    doSave() {
        this.service.saveEnrollee(this.model);
    }
}