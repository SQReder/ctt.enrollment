import {NgModule} from "@angular/core"
import {RouterModule} from "@angular/router"
import {AdmissionComponent} from "./root";
import {AdmissionListComponent} from "./list";
import {AdmissionEditComponent} from "./edit";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'admission',
        component: AdmissionComponent,
        children: [
          {path: '', component: AdmissionListComponent},
          {path: "new", component: AdmissionEditComponent},
          {path: "edit/:id", component: AdmissionEditComponent},
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class AdmissionRoutingModule {
}
