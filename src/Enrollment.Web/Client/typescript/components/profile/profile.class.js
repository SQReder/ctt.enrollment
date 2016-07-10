"use strict";
var Profile = (function () {
    function Profile() {
    }
    Profile.prototype.onProfileInfoLoaded = function (result) {
        console.log(result);
        this.info = result.user;
    };
    Profile.prototype.onListEnrolleeLoaded = function (result) {
        console.log(result);
        this.enrollees = result.enrollees;
    };
    return Profile;
}());
exports.Profile = Profile;
