"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
(function (RelationTypeEnum) {
    RelationTypeEnum[RelationTypeEnum["Child"] = 0] = "Child";
    RelationTypeEnum[RelationTypeEnum["Grandchild"] = 1] = "Grandchild";
    RelationTypeEnum[RelationTypeEnum["Ward"] = 2] = "Ward";
})(exports.RelationTypeEnum || (exports.RelationTypeEnum = {}));
var RelationTypeEnum = exports.RelationTypeEnum;
/*
 * Raise the value exponentially
 * Takes an exponent argument that defaults to 1.
 * Usage:
 *   value | exponentialStrength:exponent
 * Example:
 *   {{ 2 |  exponentialStrength:10}}
 *   formats to: 1024
*/
var RelationTypeStringPipe = (function () {
    function RelationTypeStringPipe() {
    }
    RelationTypeStringPipe.prototype.transform = function (relation) {
        switch (relation) {
            case RelationTypeEnum.Child:
                return "Сын/Дочь";
            case RelationTypeEnum.Grandchild:
                return "Внук/Внучка";
            case RelationTypeEnum.Ward:
                return "Подопечный";
            default:
                throw new RangeError("Unsupported relation value " + relation);
        }
    };
    RelationTypeStringPipe = __decorate([
        core_1.Pipe({ name: "relationType" })
    ], RelationTypeStringPipe);
    return RelationTypeStringPipe;
}());
exports.RelationTypeStringPipe = RelationTypeStringPipe;
