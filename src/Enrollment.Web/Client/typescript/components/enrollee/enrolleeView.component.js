"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var EnrolleeViewComponent = (function () {
    function EnrolleeViewComponent(service, routeParams) {
        this.service = service;
        this.routeParams = routeParams;
        this.requestRemove = new core_1.EventEmitter();
        this.requestEdit = new core_1.EventEmitter();
    }
    EnrolleeViewComponent.prototype.onRemoveButtonClicked = function () {
        this.requestRemove.emit(this.model.id);
    };
    EnrolleeViewComponent.prototype.onEditButtonClicked = function () {
        this.requestEdit.emit(this.model.id);
    };
    __decorate([
        core_1.Input()
    ], EnrolleeViewComponent.prototype, "model", void 0);
    __decorate([
        core_1.Output()
    ], EnrolleeViewComponent.prototype, "requestRemove", void 0);
    __decorate([
        core_1.Output()
    ], EnrolleeViewComponent.prototype, "requestEdit", void 0);
    EnrolleeViewComponent = __decorate([
        core_1.Component({
            selector: "enroll-enrollee-view",
            templateUrl: "/Enrollee/ViewLayout"
        })
    ], EnrolleeViewComponent);
    return EnrolleeViewComponent;
}());
exports.EnrolleeViewComponent = EnrolleeViewComponent;
