"use strict";
var User = (function () {
    function User(username, roles) {
        this.username = username;
        this.roles = roles;
    }
    return User;
}());
exports.User = User;
