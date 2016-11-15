import {NgModule} from "@angular/core"
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {EnrolleeListComponent} from "./enrollee-list-component";
import {EnrolleeService} from "./enrollee-service";
import {EnrolleeRoutingModule} from "./enrollee-routing.module";
import {EnrolleeComponent} from "./enrollee-component";
import {EnrolleeShortInfoComponent} from "./enrollee-short-info-component";
import {EnrolleeEditComponent} from "./enrollee-edit-component";
import {RelationTypeStringPipe} from "./relation-type-pipe";


@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    EnrolleeRoutingModule
  ],
  declarations: [
    EnrolleeComponent,
    EnrolleeListComponent,
    EnrolleeShortInfoComponent,
    EnrolleeEditComponent,
    RelationTypeStringPipe,
  ],
  providers: [
    EnrolleeService
  ]
})
export class EnrolleeModule {
}
