import {Component, Input, Output, EventEmitter} from 'angular2/core'
import {FormBuilder, Control, ControlGroup} from 'angular2/common'
import {ChildItem} from './childItem.class';

@Component({
    selector: 'enroll-profile-child-item',
    templateUrl: '/Profile/ChildItemLayout',
})
export class ChildItemComponent {
    @Input() model: ChildItem;
    @Output() requestRemove = new EventEmitter<string>();

    editMode = false;

    form: ControlGroup;
    firstName: Control;
    middleName: Control;
    lastName: Control;

    constructor(
    ) {        
        this.firstName = new Control(null);
        this.middleName = new Control(null);
        this.lastName = new Control(null);
    }

    onRemoveButtonClicked() {
        this.requestRemove.emit(this.model.id);
    }
}