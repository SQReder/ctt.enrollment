import  {Component, Input, Output, EventEmitter} from "@angular/core"
import {Enrollee} from "./enrollee-service";

@Component({
  selector: "ctt-enrollee-short-info",
  templateUrl: "enrollee-short-info-component.html"
})
export class EnrolleeShortInfoComponent {
  @Input() model: Enrollee;
  @Output() requestEdit = new EventEmitter<string>();
  @Output() requestRemove = new EventEmitter<string>();

  onRemoveButtonClicked() {
    this.requestRemove.emit(this.model.id);
  }
  onEditButtonClicked() {
    this.requestEdit.emit(this.model.id);
  }
}
