import {Component, Input, Output, EventEmitter} from '@angular/core'
import {CORE_DIRECTIVES} from '@angular/common'
import {ProfileService} from '../../shared/profile/profile.service'
import {EnrolleeList} from './enrolleeList.class';
import {EnrolleeItem} from './enrolleeItem.class';
import {EnrolleeItemComponent} from "./enrolleeItem.component";

@Component({
    selector: 'enroll-profile-children',
    templateUrl: '/Profile/ChildListLayout',
    directives: [
        CORE_DIRECTIVES,
        EnrolleeItemComponent
    ]
})
export class EnrolleeListComponent {
    @Input() model: EnrolleeItem[];
    @Output() requestRemove = new EventEmitter<string>();

    constructor(
        private profileService: ProfileService
    ) {
    }

    onRequestRemove(id: string) {
        this.requestRemove.emit(id);
    }
}