"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var enrollee_class_1 = require("./enrollee.class");
var relationType_enum_1 = require("./relationType.enum");
var router_deprecated_1 = require("@angular/router-deprecated");
var radioControlValueAccessor_directive_1 = require("../../shared/directives/radioControlValueAccessor.directive");
var EnrolleeEditComponent = (function () {
    function EnrolleeEditComponent(routeParams, service) {
        this.routeParams = routeParams;
        this.service = service;
        this.model = new enrollee_class_1.Enrollee();
        this.submitted = false;
        this.relations = [
            relationType_enum_1.RelationTypeEnum.Child,
            relationType_enum_1.RelationTypeEnum.Grandchild,
            relationType_enum_1.RelationTypeEnum.Ward
        ];
    }
    EnrolleeEditComponent.prototype.onSubmit = function () { this.submitted = true; };
    Object.defineProperty(EnrolleeEditComponent.prototype, "diagnostic", {
        // TODO: Remove this when we're done
        get: function () { return JSON.stringify(this.model); },
        enumerable: true,
        configurable: true
    });
    EnrolleeEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        var id = this.routeParams.get("id");
        if (id !== undefined) {
            this.service.getEnrollee(id)
                .subscribe(function (result) {
                var enrollee = result.enrollee;
                if (enrollee == null)
                    enrollee = new enrollee_class_1.Enrollee();
                _this.applyModel(enrollee);
            });
        }
        else {
            this.applyModel(new enrollee_class_1.Enrollee());
        }
    };
    EnrolleeEditComponent.prototype.applyModel = function (model) {
        this.model = model;
        this.initialized = true;
    };
    EnrolleeEditComponent.prototype.doSave = function () {
        this.service.saveEnrollee(this.model);
    };
    EnrolleeEditComponent = __decorate([
        core_1.Component({
            selector: "enroll-enrollee-edit",
            templateUrl: "/Enrollee/EditLayout",
            directives: [
                router_deprecated_1.ROUTER_DIRECTIVES,
                radioControlValueAccessor_directive_1.RadioControlValueAccessor
            ],
            pipes: [relationType_enum_1.RelationTypeStringPipe]
        })
    ], EnrolleeEditComponent);
    return EnrolleeEditComponent;
}());
exports.EnrolleeEditComponent = EnrolleeEditComponent;
