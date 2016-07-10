"use strict";
var http_1 = require("@angular/http");
exports.requestParams = function () {
    var params = new http_1.URLSearchParams();
    params.set("__RequestVerificationToken", $("input[type='hidden'][name='__RequestVerificationToken']").val());
    return params;
};
