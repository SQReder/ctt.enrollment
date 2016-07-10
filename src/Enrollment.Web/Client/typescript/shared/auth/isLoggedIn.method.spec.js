"use strict";
/// <reference path="../../../../typings/main.d.ts"/>
var app_injector_1 = require("../../app.injector");
var isLoggedIn_method_1 = require("./isLoggedIn.method");
describe("isLoggedIn", function () {
    it("should return 'auth.check' result with url from 'to' parameter", function () {
        var authService = jasmine.createSpyObj("authService", ["check"]);
        authService.check.and.returnValue(true);
        var injector = jasmine.createSpyObj("injector", ["get"]);
        injector.get.and.returnValue(authService);
        app_injector_1.appInjector(injector);
        var to = jasmine.createSpy("ComponentInstruction");
        to.urlPath = "/connection";
        var result = isLoggedIn_method_1.isLoggedIn(to, null);
        expect(authService.check).toHaveBeenCalledTimes(1);
        expect(authService.check).toHaveBeenCalledWith("/connection");
        expect(result).toBe(true);
    });
});
