import {Component, Input, Output, EventEmitter} from "@angular/core"
import {RouteParams} from "@angular/router-deprecated"
import {Enrollee} from "./enrollee.class";
import {EnrolleeService} from "../../shared/enrollee/enrollee.service";

@Component({
    selector: "enroll-enrollee-view",
    templateUrl: "/Enrollee/ViewLayout"
})
export class EnrolleeViewComponent {
    @Input() model: Enrollee;
    @Output() requestRemove = new EventEmitter<string>();
    @Output() requestEdit = new EventEmitter<string>();


    constructor(
        private service: EnrolleeService,
        private routeParams: RouteParams
    ) {
    }

    onRemoveButtonClicked() {
        this.requestRemove.emit(this.model.id);
    }

    onEditButtonClicked() {
        this.requestEdit.emit(this.model.id);
    }
}