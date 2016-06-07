import {Injectable, OnInit} from '@angular/core'
import {ProfileService} from '../../shared/profile/profile.service';
import {EnrolleeItem} from './enrolleeItem.class';
import * as HttpResults from "../../shared/responses/httpResults";

@Injectable()
export class EnrolleeList implements OnInit {
    items: EnrolleeItem[] = [];

    constructor(
        private profileService: ProfileService
    ) {
    }

    ngOnInit() {
        this.profileService
            .listChildren()
            .subscribe((result: HttpResults.ListEnrolleeResult) => {
                if (result.succeeded) {
                    this.items = result.children;
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