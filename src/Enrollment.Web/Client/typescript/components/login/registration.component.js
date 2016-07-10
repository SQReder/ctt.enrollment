"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var RegistrationComponent = (function () {
    function RegistrationComponent(formBuilder, authService, router) {
        this.authService = authService;
        this.router = router;
        this.firstname = new common_1.Control("Василий", common_1.Validators.required);
        this.lastname = new common_1.Control("Пупкин", common_1.Validators.required);
        this.middlename = new common_1.Control("Иванович");
        this.job = new common_1.Control('ООО "Рога и Копыта"', common_1.Validators.required);
        this.jobPosition = new common_1.Control("Директор", common_1.Validators.required);
        this.phone = new common_1.Control("+71235678901", common_1.Validators.required);
        this.email = new common_1.Control("pupkinv@riko.ru", common_1.Validators.pattern(".*"));
        this.address = new common_1.Control("Москва", common_1.Validators.required);
        this.registrationForm = formBuilder.group({
            firstname: this.firstname,
            lastname: this.lastname,
            middlename: this.middlename,
            job: this.job,
            jobPosition: this.jobPosition,
            phone: this.phone,
            email: this.email,
            address: this.address,
        });
    }
    RegistrationComponent.prototype.doRegister = function () {
        var _this = this;
        var model = this.registrationForm.value;
        console.log(model);
        this.authService.register(model)
            .subscribe(function (result) {
            console.log(result);
            _this.router.navigate(["Login"]);
        });
        event.preventDefault();
    };
    RegistrationComponent = __decorate([
        core_1.Component({
            selector: "enroll-registration",
            templateUrl: "/Account/Registration",
            providers: [
                common_1.FormBuilder
            ],
        })
    ], RegistrationComponent);
    return RegistrationComponent;
}());
exports.RegistrationComponent = RegistrationComponent;
