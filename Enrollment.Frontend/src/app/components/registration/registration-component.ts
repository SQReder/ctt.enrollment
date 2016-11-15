import {Component} from "@angular/core";
import {RegistrationModel} from "../../services/auth/registration-model";
import {AuthService} from "../../services/auth/auth-service";
import {Subject} from "rxjs";

@Component({
  selector: "ctt-registration",
  templateUrl: "registration-component.html"
})
export class RegistrationComponent {
  model: RegistrationModel = new RegistrationModel();
  private isUsernameUsedValue: boolean;
  private checkUsernameStream = new Subject<string>();
  private pendingUsernameCheckValue;

  constructor(
    private authService: AuthService
  ) {
    //noinspection TypeScriptUnresolvedFunction
    this.checkUsernameStream
      .distinctUntilChanged()
      .debounceTime(300)
      .switchMap((username: string) => {
        this.pendingUsernameCheckValue = true;
        return this.authService.checkUsername(username);
      })
      .subscribe((result: CheckUsernameApiResult) => {
        this.isUsernameUsedValue = result.isUsed;
        this.pendingUsernameCheckValue = false;
      });
  }

  checkUsername() {
    this.checkUsernameStream.next(this.model.username);
  }

  get isUsernameUsed(): boolean {
    return this.isUsernameUsedValue;
  }

  get pendingUsernameCheck(): boolean {
    return this.pendingUsernameCheckValue;
  }

  register(): Promise<never> {
    return this.authService.register(this.model)
      .then(result => {
        if (result) {
          this.authService.login({
            username: this.model.username,
            password: this.model.password,
            rememberMe: true,
            returnUrl: "/"
          });
        }
      });
  }
}
