import {Injectable, OnInit} from 'angular2/core'
import {ProfileService} from '../../shared/profile/profile.service';
import {ChildItem} from './childItem.class';
import * as HttpResults from "../../shared/responses/httpResults";

@Injectable()
export class ChildList implements OnInit {
    items: ChildItem[] = [];

    constructor(
        private profileService: ProfileService
    ) {
    }

    ngOnInit() {
        this.profileService
            .listChildren()
            .subscribe((result: HttpResults.ListChildrenResult) => {
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