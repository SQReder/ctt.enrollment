"use strict";
var auth_service_1 = require("./auth.service");
var app_injector_1 = require("../../app.injector");
exports.isLoggedIn = function (to, from) {
    var injector = app_injector_1.appInjector();
    var auth = injector.get(auth_service_1.AuthService);
    return auth.check(to.urlPath);
};
