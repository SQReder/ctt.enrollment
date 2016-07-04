import {Component} from '@angular/core'
import {RouteConfig, ROUTER_DIRECTIVES} from '@angular/router-deprecated'

import {EnrolleeListComponent} from './enrolleeList.component'
import {EnrolleeEditComponent} from "./enrolleeEdit.component";
import {EnrolleeViewComponent} from "./enrolleeView.component";

@Component({
    selector: 'enroll-enrolles-component',
    templateUrl: '/Enrollee/Layout',
    directives: [
        ROUTER_DIRECTIVES
    ]
})
@RouteConfig([
    {
        path: '/list',
        component: EnrolleeListComponent,
        name: 'List',
        useAsDefault: true
    },
    {
        path: '/edit/:id',
        component: EnrolleeEditComponent,
        name: 'Edit'
    },
    {
        path: '/view/:id',
        component: EnrolleeViewComponent,
        name: 'View'
    }
])
export class EnrolleesComponent {
}