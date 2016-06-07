import {Component} from '@angular/core'
import {FormBuilder, Control, Validators, ControlGroup} from '@angular/common'
import {RegistrationModel} from '../../shared/auth/registrationModel.class';
import {AuthService} from "../../shared/auth/auth.service";

@Component({
    selector: 'enroll-registration',
    templateUrl: '/Account/Registration',
    providers: [
        FormBuilder
    ],
})
export class RegistrationComponent {
    firstname: Control;
    lastname: Control;
    middlename: Control;
    job: Control;
    jobPosition: Control;
    phone: Control;
    email: Control;
    address: Control;
    registrationForm: ControlGroup;

    constructor(
        formBuilder: FormBuilder,
        private authService: AuthService
    ) {
        this.firstname = new Control('Василий', Validators.required);
        this.lastname = new Control('Пупкин', Validators.required);
        this.middlename = new Control('Иванович');
        this.job = new Control('ООО "Рога и Копыта"', Validators.required);
        this.jobPosition = new Control('Директор', Validators.required);
        this.phone = new Control('+71235678901', Validators.required);
        this.email = new Control('pupkinv@riko.ru', Validators.pattern('.*'));
        this.address = new Control('Москва', Validators.required);
        this.registrationForm = formBuilder.group({
            'firstname': this.firstname,
            'lastname': this.lastname,
            'middlename': this.middlename,
            'job': this.job,
            'jobPosition': this.jobPosition,
            'phone': this.phone,
            'email': this.email,
            'address': this.address,
        });
    }

    doRegister() {
        const model: RegistrationModel = this.registrationForm.value;
        console.log(model);
        this.authService.register(model).subscribe(result => {
            console.log(result);
        });
        event.preventDefault();
    }
}