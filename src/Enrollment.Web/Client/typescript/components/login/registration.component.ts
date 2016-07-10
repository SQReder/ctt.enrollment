import {Component} from "@angular/core"
import {Router} from "@angular/router-deprecated"
import {RegistrationModel} from "../../shared/auth/registrationModel.class";
import {AuthService} from "../../shared/auth/auth.service";

@Component({
    selector: "enroll-registration",
    templateUrl: "/Account/Registration"
})
export class RegistrationComponent {
    model: RegistrationModel;

    constructor(
        private authService: AuthService,
        private router: Router
    ) {
        this.model = new RegistrationModel();

        this.model.firstname = "Василий";        
        this.model.lastname = "Пупкин";
        this.model.middlename = "Иванович";
        this.model.job = 'ООО "Рога и Копыта"';
        this.model.jobPosition = "Директор";
        this.model.phone = "+71235678901";
        this.model.email = "pupkinv@riko.ru";
        this.model.address = "Москва";
    }

    doRegister() {
        console.log(this.model);
        this.authService.register(this.model)
            .subscribe(result => {
                console.log(result);
                this.router.navigate(["Login"]);
            });
        event.preventDefault();
    }
}