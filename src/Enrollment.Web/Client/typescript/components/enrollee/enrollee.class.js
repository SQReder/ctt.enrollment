"use strict";
var Address = (function () {
    function Address() {
    }
    return Address;
}());
exports.Address = Address;
var Enrollee = (function () {
    function Enrollee() {
        this.address = new Address();
    }
    return Enrollee;
}());
exports.Enrollee = Enrollee;
