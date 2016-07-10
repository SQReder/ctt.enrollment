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
var BaseService = (function () {
    function BaseService(http) {
        this.http = http;
    }
    BaseService.prototype.observableGet = function (url) {
        var _this = this;
        var headers = headers_const_1.contentHeaders;
        return Observable_1.Observable.create(function (observer) {
            _this.http
                .get(url, { headers: headers })
                .subscribe(function (responce) {
                observer.next(responce.json());
                observer.complete();
            });
        });
    };
    BaseService.prototype.observablePost = function (url, params) {
        var _this = this;
        var headers = headers_const_1.contentHeaders;
        return Observable_1.Observable.create(function (observer) {
            _this.http
                .post(url, params.toString(), { headers: headers })
                .subscribe(function (responce) {
                observer.next(responce.json());
                observer.complete();
            });
        });
    };
    BaseService = __decorate([
        core_1.Injectable()
    ], BaseService);
    return BaseService;
}());
exports.BaseService = BaseService;
