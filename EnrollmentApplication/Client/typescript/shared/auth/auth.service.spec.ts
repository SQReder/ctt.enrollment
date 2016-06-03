/// <reference path="../../../../typings/main.d.ts"/>
"use strict";

import {describe, expect, beforeEach, it, inject, injectAsync, beforeEachProviders, MockApplicationRef} from "angular2/testing"
import {provide, ApplicationRef} from 'angular2/core';
import {PromiseWrapper} from 'angular2/src/facade/async';
import {MockBackend} from 'angular2/http/testing';
import {MockConnection} from 'angular2/src/http/backends/mock_backend';

import {Router, ROUTER_PROVIDERS, ROUTER_PRIMARY_COMPONENT, HashLocationStrategy, LocationStrategy} from 'angular2/router';

import {HTTP_PROVIDERS, XHRBackend, Response, ResponseOptions} from 'angular2/http';

import {contentHeaders} from '../requests/headers.const';
import {requestParams} from '../requests/params.method';
import {AuthService} from "./auth.service"

describe("AuthService", () => {
    var authService: AuthService;
    let injector,
        backend,
        mockBackend,
        httpService;

    class MockPrimaryComponent {
    }

    beforeEachProviders(() => {
        return [
            AuthService,
            ROUTER_PROVIDERS,
            provide(LocationStrategy, { useClass: HashLocationStrategy }),
            provide(ROUTER_PRIMARY_COMPONENT, { useClass: MockPrimaryComponent }),
            HTTP_PROVIDERS,
            provide(XHRBackend, { useClass: MockBackend }),
            provide(ApplicationRef, { useClass: MockApplicationRef })
        ];
    });

    beforeEach(() => {
        spyOn(localStorage, "setItem");
        spyOn(localStorage, "removeItem");
    });

    it("check should return true if localStorage contains user token", inject([AuthService, Router], (authService: AuthService, router: Router) => {
        var getItemSpyOn = spyOn(localStorage, "getItem");
        spyOn(router, "navigate");

        getItemSpyOn.and.returnValue(null);
        var result = authService.check();
        expect(result).toBe(false);

        getItemSpyOn.and.returnValue("token");
        var result = authService.check();
        expect(result).toBe(true);
    }));

    it("check should navigate to Login if localStorage doesn't contain user token", inject([AuthService, Router], (authService: AuthService, router: Router) => {
        spyOn(localStorage, "getItem").and.returnValue(null);

        var navigateSpyOn = spyOn(router, "navigate");

        var result = authService.check();
        expect(result).toBe(false);
        expect(navigateSpyOn).toHaveBeenCalledWith(["/Login"]);

        var result = authService.check("connection");
        expect(result).toBe(false);
        expect(navigateSpyOn).toHaveBeenCalledWith(["/Login", { "returnUrl": "connection" }]);
    }));

    it("login method should store user token in localStorage and emit onIsLoggedInChanged event with 'true' if login is succeeded",
        inject([XHRBackend, AuthService], (mockBackend, authService) => {
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Roles":["Admin"],"Succeeded":true,"Message":"Authentication succeeded"}' })));
                });
            spyOn(authService.onIsLoggedInChanged, "emit");

            authService.login("test@mail.com", "123", "http://returnurl");

            expect(localStorage.setItem).toHaveBeenCalledTimes(1);
            expect(localStorage.setItem).toHaveBeenCalledWith("user", '{"username":"test@mail.com","roles":["Admin"]}');
            expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledTimes(1);
            expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledWith(true);
        }));

    it("login method should not store user token in localStorage emit onIsLoggedInChanged event if login is failed",
        inject([XHRBackend, AuthService], (mockBackend, authService) => {
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Succeeded":false,"Message":"Authentication failed"}' })));
                });
            spyOn(authService.onIsLoggedInChanged, "emit");

            authService.login("test@mail.com", "123", "http://returnurl");

            expect(localStorage.setItem).not.toHaveBeenCalled();
            expect(authService.onIsLoggedInChanged.emit).not.toHaveBeenCalled();
        }));

    it("login method should navigate to returnUrl if login is succeeded",
        inject([XHRBackend, AuthService, Router], (mockBackend, authService, router) => {
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
                });

            var navigateSpyOn = spyOn(router, "navigateByUrl");

            authService.login("test@mail.com", "123", "/connection");
            expect(navigateSpyOn).toHaveBeenCalledWith("/connection");

            authService.login("test@mail.com", "123");
            expect(navigateSpyOn).toHaveBeenCalledWith("/");
        }));

    it("logout method should remove user token from localStorage and emit onIsLoggedInChanged event with 'true' if logout is succeeded",
        inject([XHRBackend, AuthService, Router], (mockBackend, authService: AuthService, router: Router) => {
            spyOn(authService.onIsLoggedInChanged, "emit");
            spyOn(router, "navigate");
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
                });

            authService.logout();

            expect(localStorage.removeItem).toHaveBeenCalledTimes(1);
            expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledTimes(1);
            expect(authService.onIsLoggedInChanged.emit).toHaveBeenCalledWith(false);
        })
    );

    it("logout method should not remove user token from localStorage and emit onIsLoggedInChanged event with 'true' if logout is faied",
        inject([XHRBackend, AuthService, Router], (mockBackend, authService: AuthService, router: Router) => {

            spyOn(authService.onIsLoggedInChanged, "emit");
            spyOn(router, "navigate");
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Succeeded":false}' })));
                });

            authService.logout();

            expect(localStorage.removeItem).not.toHaveBeenCalled();
            expect(authService.onIsLoggedInChanged.emit).not.toHaveBeenCalled();
        })
    );

    it("logout method should navigate to Login if logout is succeeded",
        inject([XHRBackend, AuthService, Router], (mockBackend, authService, router) => {
            mockBackend.connections.subscribe(
                (connection: MockConnection) => {
                    connection.mockRespond(new Response(
                        new ResponseOptions({ status: 200, body: '{"Succeeded":true}' })));
                });

            var navigateSpyOn = spyOn(router, "navigate");

            authService.logout();
            expect(navigateSpyOn).toHaveBeenCalledWith(["Login"]);
        }));
});