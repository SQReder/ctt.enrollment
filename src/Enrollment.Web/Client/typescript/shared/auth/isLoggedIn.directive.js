"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var IsLoggedInDirective = (function () {
    function IsLoggedInDirective(viewContainer, templateRef, authService) {
        var _this = this;
        this.viewContainer = viewContainer;
        this.templateRef = templateRef;
        this.authService = authService;
        this.updateVisibility(authService.check());
        authService.onIsLoggedInChanged.subscribe(function (isLoggedIn) { return _this.onIsLoggedInChangedHandler(isLoggedIn); });
    }
    IsLoggedInDirective.prototype.onIsLoggedInChangedHandler = function (isLoggedIn) {
        this.updateVisibility(isLoggedIn);
    };
    IsLoggedInDirective.prototype.updateVisibility = function (visible) {
        if (visible) {
            this.viewContainer.createEmbeddedView(this.templateRef);
        }
        else {
            this.viewContainer.clear();
        }
    };
    IsLoggedInDirective = __decorate([
        core_1.Directive({
            selector: "[isLoggedIn]"
        })
    ], IsLoggedInDirective);
    return IsLoggedInDirective;
}());
exports.IsLoggedInDirective = IsLoggedInDirective;
