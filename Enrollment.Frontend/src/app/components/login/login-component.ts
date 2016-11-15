import {Component, Inject} from "@angular/core";
import {AuthService} from "../../services/auth/auth-service";
import {Router} from "@angular/router";
import {LoginModel} from "../../services/auth/login-model";
import {ProfileService} from "../../services/api/profile-service";
import {TrusteeService} from "../../services/api/trustee-service";
import {LocalStorage} from "../../services/local-storage";

@Component({
  selector: "ctt-login",
  templateUrl: "login-component.html"
})
export class LoginComponent {
  phoneExtractionRegex = /\+7 \((\d\d\d)\) (\d\d\d)-(\d\d)-(\d\d)/;

  phoneMask = ["+", "7", " ", "(", /\d/, /\d/, /\d/, ")", " ", /\d/, /\d/, /\d/, "-", /\d/, /\d/, "-", /\d/, /\d/];

  model: LoginModel;

  constructor(
    private auth: AuthService,
    private router: Router,
    private profileService: ProfileService,
    @Inject(LocalStorage) private localStorage
  ) {
    this.model = new LoginModel();
    this.model.username = "admin";
    this.model.password = "p@55w0rD";
  }

  private phoneExtractor(str, code, p1, p2, p3) {
    return "7" + code + p1 + p2 + p3;
  }

  login(): Promise<any> {
    const username = this.model.username.replace(this.phoneExtractionRegex, this.phoneExtractor);

    const loginModel: LoginModel = new LoginModel();
    loginModel.username = username;
    loginModel.password = this.model.password;
    loginModel.rememberMe = true;

    return this.auth.login(loginModel)
      .then((success: boolean) => {
        if (success) {
          this.router.navigate(["/"]);
        }
      })
      .then(() => {
        return this.profileService.read(this.auth.userId)
          .then(profile => {
            this.localStorage.setItem("trusteeId", profile.trusteeId)
          });
      })
  }
}
