import {Component, Input, Output, EventEmitter} from '@angular/core'
import {EnrolleeItem} from './enrolleeItem.class';

@Component({
    selector: 'enroll-profile-childitem-editor',
    templateUrl: '/Profile/ChildItemEditorLayout'
})
export class ChildItemEditor {
    @Input() model: EnrolleeItem;
    @Output() submit: EventEmitter<EnrolleeItem>;

    constructor(
    ) {         
    }

    onSubmit() {
        this.submit.emit(this.model);
    }
}