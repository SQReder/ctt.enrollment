import {Component, Input, Output, EventEmitter} from 'angular2/core'
import {ChildItem} from './childItem.class';

@Component({
    selector: 'enroll-profile-childitem-editor',
    templateUrl: '/Profile/ChildItemEditorLayout'
})
export class ChildItemEditor {
    @Input() model: ChildItem;
    @Output() submit: EventEmitter<ChildItem>;

    constructor(
    ) {         
    }

    onSubmit() {
        this.submit.emit(this.model);
    }
}