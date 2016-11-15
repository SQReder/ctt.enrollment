import {NgModule} from "@angular/core"
import {RouterModule} from "@angular/router"
import {EnrolleeListComponent} from "./enrollee-list-component";
import {EnrolleeComponent} from "./enrollee-component";
import {EnrolleeEditComponent} from "./enrollee-edit-component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'enrollee',
        component: EnrolleeComponent,
        children: [
          {path: '', component: EnrolleeListComponent},
          {path: "new", component: EnrolleeEditComponent},
          {path: "edit/:id", component: EnrolleeEditComponent},
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class EnrolleeRoutingModule {
}
