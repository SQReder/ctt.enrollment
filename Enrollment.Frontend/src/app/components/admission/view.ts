import {Component, Input, Output, EventEmitter} from "@angular/core";
import {Admission} from "./admission-class";

@Component({
  selector: "ctt-admission-view",
  templateUrl: "view.html"
})
export class AdmissionViewComponent {
  @Input() model: Admission;
  @Output() requestRemove = new EventEmitter<string>();

  onRequestRemoveClicked() {
    this.requestRemove.emit(this.model.id);
  }
}
