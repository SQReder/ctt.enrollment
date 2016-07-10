"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var profileView_component_1 = require("./profileView.component");
var ProfileComponent = (function () {
    function ProfileComponent() {
    }
    ProfileComponent = __decorate([
        core_1.Component({
            selector: "enroll-profile",
            templateUrl: "/Profile/Index",
            directives: [
                router_deprecated_1.ROUTER_DIRECTIVES
            ]
        }),
        router_deprecated_1.RouteConfig([
            {
                path: "view",
                component: profileView_component_1.ProfileViewComponent,
                name: "View",
                useAsDefault: true
            }
        ])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
