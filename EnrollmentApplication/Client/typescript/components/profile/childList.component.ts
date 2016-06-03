import {Component, Input, Output, EventEmitter} from 'angular2/core'
import {CORE_DIRECTIVES} from 'angular2/common'
import {ProfileService} from '../../shared/profile/profile.service'
import {ChildList} from './childList.class';
import {ChildItem} from './childItem.class';
import {ChildItemComponent} from "./childItem.component";

@Component({
    selector: 'enroll-profile-children',
    templateUrl: '/Profile/ChildListLayout',
    directives: [
        CORE_DIRECTIVES,
        ChildItemComponent
    ]
})
export class ChildListComponent {
    @Input() model: ChildItem[];
    @Output() requestRemove = new EventEmitter<string>();

    constructor(
        private profileService: ProfileService
    ) {
    }

    onRequestRemove(id: string) {
        this.requestRemove.emit(id);
    }
}