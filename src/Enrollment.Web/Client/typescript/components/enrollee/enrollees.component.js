"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var enrolleeList_component_1 = require("./enrolleeList.component");
var enrolleeEdit_component_1 = require("./enrolleeEdit.component");
var enrolleeView_component_1 = require("./enrolleeView.component");
var EnrolleesComponent = (function () {
    function EnrolleesComponent() {
    }
    EnrolleesComponent = __decorate([
        core_1.Component({
            selector: "enroll-enrolles-component",
            templateUrl: "/Enrollee/Layout",
            directives: [
                router_deprecated_1.ROUTER_DIRECTIVES
            ]
        }),
        router_deprecated_1.RouteConfig([
            {
                path: "/list",
                component: enrolleeList_component_1.EnrolleeListComponent,
                name: "List",
                useAsDefault: true
            },
            {
                path: "/edit/:id",
                component: enrolleeEdit_component_1.EnrolleeEditComponent,
                name: "Edit"
            },
            {
                path: "/view/:id",
                component: enrolleeView_component_1.EnrolleeViewComponent,
                name: "View"
            }
        ])
    ], EnrolleesComponent);
    return EnrolleesComponent;
}());
exports.EnrolleesComponent = EnrolleesComponent;
