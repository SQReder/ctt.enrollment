"use strict";
var appInjectorRef;
exports.appInjector = function (injector) {
    if (!injector || injector == null) {
        return appInjectorRef;
    }
    appInjectorRef = injector;
    return appInjectorRef;
};
