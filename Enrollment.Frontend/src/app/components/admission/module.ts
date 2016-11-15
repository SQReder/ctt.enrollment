import {NgModule} from "@angular/core"
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {AdmissionRoutingModule} from "./routing.module";
import {AdmissionComponent} from "./root";
import {AdmissionService} from "./service";
import {AdmissionListComponent} from "./list";
import {AdmissionViewComponent} from "./view";
import {AdmissionEditComponent} from "./edit";


@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    AdmissionRoutingModule
  ],
  declarations: [
    AdmissionComponent,
    AdmissionListComponent,
    AdmissionViewComponent,
    AdmissionEditComponent,
  ],
  providers: [
    AdmissionService
  ]
})
export class AdmissionModule {
}
