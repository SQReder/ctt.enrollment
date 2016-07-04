import {Injectable, OnInit} from '@angular/core'
import {EnrolleeService} from '../../shared/enrollee/enrollee.service';
import {Enrollee} from './enrollee.class';
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
            .subscribe((result: HttpResults.ListEnrolleeResult) => {
                if (result.succeeded) {
                    this.items = result.enrollees;
                }
            });
    }

    getList() {
        return this.items;
    }

    onRequestRemove(id: string) {
        console.log('remove requested', id);
    }
}