"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var Observable_1 = require("rxjs/Observable");
var headers_const_1 = require("../requests/headers.const");
var params_method_1 = require("../requests/params.method");
var user_class_1 = require("./user.class");
var AuthService = (function () {
    function AuthService(router, http) {
        this.router = router;
        this.http = http;
        this.tokenName = "user";
        this.onIsLoggedInChanged = new core_1.EventEmitter();
    }
    AuthService.prototype.check = function (returnUrl) {
        if (!localStorage.getItem(this.tokenName)) {
            if (returnUrl != null) {
                this.router.navigate(["Login", { "returnUrl": returnUrl }]);
            }
            else {
                this.router.navigate(["Login"]);
            }
            return false;
        }
        else {
            return true;
        }
    };
    AuthService.prototype.login = function (username, password, returnUrl) {
        var _this = this;
        returnUrl = returnUrl == null ? "/" : returnUrl;
        var headers = headers_const_1.contentHeaders;
        var params = params_method_1.requestParams();
        params.set("username", username);
        params.set("password", password);
        params.set("rememberMe", true.toString());
        this.http.post("./Account/Login", params.toString(), { headers: headers }).subscribe(function (response) {
            var loginResult = response.json();
            if (loginResult.succeeded) {
                var userToken = JSON.stringify(new user_class_1.User(username, loginResult.roles));
                localStorage.setItem(_this.tokenName, userToken);
                _this.onIsLoggedInChanged.emit(true);
                _this.router.navigateByUrl(returnUrl);
            }
        });
    };
    AuthService.prototype.logout = function () {
        var _this = this;
        var headers = headers_const_1.contentHeaders;
        var params = params_method_1.requestParams();
        this.http.post("./Account/Logout", params.toString(), { headers: headers }).subscribe(function (response) {
            var logoutResult = response.json();
            if (logoutResult.succeeded) {
                _this.onIsLoggedInChanged.emit(false);
                localStorage.removeItem(_this.tokenName);
                _this.router.navigate(["Login"]);
            }
        });
    };
    AuthService.prototype.register = function (model) {
        var _this = this;
        var headers = headers_const_1.contentHeaders;
        var params = params_method_1.requestParams();
        for (var key in model) {
            if (model.hasOwnProperty(key)) {
                params.set(key, model[key]);
            }
        }
        return Observable_1.Observable.create(function (observer) {
            _this.http
                .post("./Account/Registration", params.toString(), { headers: headers })
                .subscribe(function (responce) {
                observer.next(responce.json());
                observer.complete();
            });
        });
    };
    AuthService = __decorate([
        core_1.Component({
            providers: []
        }),
        core_1.Injectable()
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
