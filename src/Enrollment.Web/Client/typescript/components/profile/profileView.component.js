"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var profile_class_1 = require("./profile.class");
var profileInfo_component_1 = require("./profileInfo.component");
var ProfileViewComponent = (function () {
    function ProfileViewComponent(profileService) {
        this.profileService = profileService;
        this.model = new profile_class_1.Profile();
    }
    ProfileViewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.profileService
            .getCurrentUser()
            .subscribe(function (result) { return _this.model.onProfileInfoLoaded(result); });
    };
    ProfileViewComponent = __decorate([
        core_1.Component({
            selector: "enroll-profile-view",
            templateUrl: "/Profile/ViewLayout",
            directives: [
                profileInfo_component_1.ProfileInfoComponent
            ]
        })
    ], ProfileViewComponent);
    return ProfileViewComponent;
}());
exports.ProfileViewComponent = ProfileViewComponent;
