"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var router_deprecated_1 = require("@angular/router-deprecated");
var enrolleeView_component_1 = require("./enrolleeView.component");
var EnrolleeListComponent = (function () {
    function EnrolleeListComponent(enrolleeService, router) {
        this.enrolleeService = enrolleeService;
        this.router = router;
    }
    EnrolleeListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.enrolleeService
            .listEnrollee()
            .subscribe(function (result) {
            console.log(result);
            _this.model = result.enrollees;
        });
    };
    EnrolleeListComponent.prototype.createNew = function () {
        this.router.navigate(["/Enrollee/Edit", { id: null }]);
    };
    EnrolleeListComponent.prototype.onRequestEdit = function (id) {
        this.router.navigate(["/Enrollee/Edit", { id: id }]);
    };
    EnrolleeListComponent = __decorate([
        core_1.Component({
            selector: "enroll-enrollee-list",
            templateUrl: "/Enrollee/ListLayout",
            directives: [
                common_1.CORE_DIRECTIVES,
                router_deprecated_1.ROUTER_DIRECTIVES,
                enrolleeView_component_1.EnrolleeViewComponent
            ]
        })
    ], EnrolleeListComponent);
    return EnrolleeListComponent;
}());
exports.EnrolleeListComponent = EnrolleeListComponent;
