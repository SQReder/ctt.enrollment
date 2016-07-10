"use strict";
var testing_1 = require("@angular/core/testing");
var core_1 = require("@angular/core");
var testing_2 = require("@angular/http/testing");
var router_deprecated_1 = require("@angular/router-deprecated");
var common_1 = require("@angular/common");
var http_1 = require("@angular/http");
var auth_service_1 = require("./auth.service");
testing_1.describe("AuthService", function () {
    var authService;
    var injector, backend, mockBackend, httpService;
    var MockPrimaryComponent = (function () {
        function MockPrimaryComponent() {
        }
        return MockPrimaryComponent;
    }());
    testing_1.beforeEachProviders(function () {
        return [
            auth_service_1.AuthService,
            router_deprecated_1.ROUTER_PROVIDERS,
            core_1.provide(common_1.LocationStrategy, { useClass: common_1.HashLocationStrategy }),
            core_1.provide(router_deprecated_1.ROUTER_PRIMARY_COMPONENT, { useClass: MockPrimaryComponent }),
            http_1.HTTP_PROVIDERS,
            core_1.provide(http_1.XHRBackend, { useClass: testing_2.MockBackend }),
            core_1.provide(core_1.ApplicationRef, { useClass: testing_1.MockApplicationRef })
        ];
    });
    testing_1.beforeEach(function () {
        spyOn(localStorage, "setItem");
        spyOn(localStorage, "removeItem");
    });
    testing_1.it("check should return true if localStorage contains user token", testing_1.inject([auth_service_1.AuthService, router_deprecated_1.Router], function (authService, router) {
        var getItemSpyOn = spyOn(localStorage, "getItem");
        spyOn(router, "navigate");
        getItemSpyOn.and.returnValue(null);
        var result = authService.check();
        testing_1.expect(result).toBe(false);
        getItemSpyOn.and.returnValue("token");
        var result = authService.check();
        testing_1.expect(result).toBe(true);
    }));
    testing_1.it("check should navigate to Login if localStorage doesn't contain user token", testing_1.inject([auth_service_1.AuthService, router_deprecated_1.Router], function (authService, router) {
        spyOn(localStorage, "getItem").and.returnValue(null);
        var navigateSpyOn = spyOn(router, "navigate");
        var result = authService.check();
        testing_1.expect(result).toBe(false);
        testing_1.expect(navigateSpyOn).toHaveBeenCalledWith(["/Login"]);
        var result = authService.check("connection");
        testing_1.expect(result).toBe(false);
        testing_1.expect(navigateSpyOn).toHaveBeenCalledWith(["/Login", { "returnUrl": "connection" }]);
    }));
    testing_1.it("login method should store user token in localStorage and emit onIsLoggedInChanged event with 'true' if login is succeeded", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService], function (mockBackend, authService) {
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Roles":["Admin"],"Succeeded":true,"Message":"Authentication succeeded"}' })));
        });
        spyOn(authService.onIsLoggedInChanged, "emit");
        authService.login("test@mail.com", "123", "http://returnurl");
        testing_1.expect(localStorage.setItem).toHaveBeenCalledTimes(1);
        testing_1.expect(localStorage.setItem).toHaveBeenCalledWith("user", '{"username":"test@mail.com","roles":["Admin"]}');
        testing_1.expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledTimes(1);
        testing_1.expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledWith(true);
    }));
    testing_1.it("login method should not store user token in localStorage emit onIsLoggedInChanged event if login is failed", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService], function (mockBackend, authService) {
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Succeeded":false,"Message":"Authentication failed"}' })));
        });
        spyOn(authService.onIsLoggedInChanged, "emit");
        authService.login("test@mail.com", "123", "http://returnurl");
        testing_1.expect(localStorage.setItem).not.toHaveBeenCalled();
        testing_1.expect(authService.onIsLoggedInChanged.emit).not.toHaveBeenCalled();
    }));
    testing_1.it("login method should navigate to returnUrl if login is succeeded", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService, router_deprecated_1.Router], function (mockBackend, authService, router) {
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
        });
        var navigateSpyOn = spyOn(router, "navigateByUrl");
        authService.login("test@mail.com", "123", "/connection");
        testing_1.expect(navigateSpyOn).toHaveBeenCalledWith("/connection");
        authService.login("test@mail.com", "123");
        testing_1.expect(navigateSpyOn).toHaveBeenCalledWith("/");
    }));
    testing_1.it("logout method should remove user token from localStorage and emit onIsLoggedInChanged event with 'true' if logout is succeeded", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService, router_deprecated_1.Router], function (mockBackend, authService, router) {
        spyOn(authService.onIsLoggedInChanged, "emit");
        spyOn(router, "navigate");
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
        });
        authService.logout();
        testing_1.expect(localStorage.removeItem).toHaveBeenCalledTimes(1);
        testing_1.expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledTimes(1);
        testing_1.expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledWith(false);
    }));
    testing_1.it("logout method should not remove user token from localStorage and emit onIsLoggedInChanged event with 'true' if logout is faied", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService, router_deprecated_1.Router], function (mockBackend, authService, router) {
        spyOn(authService.onIsLoggedInChanged, "emit");
        spyOn(router, "navigate");
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Succeeded":false}' })));
        });
        authService.logout();
        testing_1.expect(localStorage.removeItem).not.toHaveBeenCalled();
        testing_1.expect(authService.onIsLoggedInChanged.emit).not.toHaveBeenCalled();
    }));
    testing_1.it("logout method should navigate to Login if logout is succeeded", testing_1.inject([http_1.XHRBackend, auth_service_1.AuthService, router_deprecated_1.Router], function (mockBackend, authService, router) {
        mockBackend.connections.subscribe(function (connection) {
            connection.mockRespond(new http_1.Response(new http_1.ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
        });
        var navigateSpyOn = spyOn(router, "navigate");
        authService.logout();
        testing_1.expect(navigateSpyOn).toHaveBeenCalledWith(["Login"]);
    }));
});
