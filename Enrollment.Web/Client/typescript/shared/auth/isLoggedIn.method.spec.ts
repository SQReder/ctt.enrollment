/// <reference path="../../../../typings/index.d.ts"/>
import {appInjector} from "../../app.injector";
import {ComponentInstruction} from "@angular/router-deprecated";
import {isLoggedIn} from "./isLoggedIn.method";

describe("isLoggedIn", () => {
    it("should return 'auth.check' result with url from 'to' parameter", () => {
        var authService = jasmine.createSpyObj("authService", ["check"]);
        authService.check.and.returnValue(true);

        var injector = jasmine.createSpyObj("injector", ["get"]);
        injector.get.and.returnValue(authService);

        appInjector(injector);

        const to: any = jasmine.createSpy("ComponentInstruction");
        to.urlPath = "/connection";

        var result = isLoggedIn(to, null);

        expect(authService.check).toHaveBeenCalledTimes(1);
        expect(authService.check).toHaveBeenCalledWith("/connection");
        expect(result).toBe(true);
    });
});