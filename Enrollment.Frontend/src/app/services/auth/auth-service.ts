import {Injectable, EventEmitter, Inject} from "@angular/core";
import {TypedHttpService} from "../http/typed-http";
import {AppOptions} from "../AppOptions";
import {RegistrationModel} from "./registration-model";
import {LoginModel} from "./login-model";
import {LocalStorage} from "../local-storage";
import {Router} from "@angular/router";

@Injectable()
export class AuthService {
  isLoggedInChanged = new EventEmitter<boolean>();
  private endpointUrl;

  constructor(
    private http: TypedHttpService,
    options: AppOptions,
    @Inject(LocalStorage) private localStorage,
    private router: Router
  ) {
    this.endpointUrl = options.apiEndpointUrl
  }

  login(model: LoginModel): Promise<boolean | string> {
    return this.http.post<AuthenticationApiResult | ErrorResult>(this.endpointUrl + "/account/login", model)
      .then((result) => {
        if (result.succeeded) {
          const authResult = result as AuthenticationApiResult;
          this.isLoggedIn = true;

          this.localStorage.setItem("roles", JSON.stringify(authResult.roles));
          this.localStorage.setItem("id", authResult.id);

          if (model.returnUrl != null) {
            this.router.navigateByUrl(model.returnUrl);
          }

          return Promise.resolve(true);
        } else {
          const error = result as ErrorResult;
          return Promise.reject(error.message);
        }
      })
      .catch((reason: any) => {
        console.log(reason);
        return false;
      });
  }

  logout(): Promise<boolean> {
    this.isLoggedIn = false;

    this.localStorage.removeItem("roles");
    this.localStorage.removeItem("loggedIn");
    this.localStorage.removeItem("id");
    this.localStorage.removeItem("trusteeId");

    this.router.navigateByUrl("/");

    return Promise.resolve(true);
  }

  get userId(): string {
    return this.localStorage.getItem("id")
  }

  get isLoggedIn(): boolean {
    const value = this.localStorage.getItem("loggedIn") || "false";
    return JSON.parse(value) === true;
  }

  get roles(): string[] {
    const rolesJSON = this.localStorage.getItem("roles") || "[]";
    const roles = JSON.parse(rolesJSON);
    return roles;
  }

  set isLoggedIn(value: boolean) {
    this.localStorage.setItem("loggedIn", value.toString());
    this.isLoggedInChanged.emit(this.isLoggedIn);
  }

  register(model: RegistrationModel): Promise<boolean> {
    return this.http.post<SuccessApiResult | ErrorResult>(this.endpointUrl + "/account/registration", model)
      .then(result => {
        if (result.succeeded) {
          return Promise.resolve(true);
        } else {
          const error = result as ErrorResult;
          return Promise.resolve(false);
        }
      }).catch(reason => {
        console.log(reason);
        return false;
      });
  }

  // register(model: RegistrationModel): Promise<boolean|string> {
  //   const params = new URLSearchParams();
  //   params.append("firstName", model.firstName);
  //   params.append("lastName", model.lastName);
  //   params.append("middleName", model.middleName);
  //   params.append("sex", model.sex.toString());
  //   params.append("job", model.job);
  //   params.append("jobPosition", model.jobPosition);
  //   params.append("phone", model.phone);
  //   params.append("email", model.email);
  //   params.append("address", model.address);
  //
  //   return this.http.post<SuccessApiResult | ErrorResult>(this.endpointUrl + '/account/profile', params.toString())
  //     .then(result => {
  //       if (result.succeeded) {
  //         return Promise.resolve(true);
  //       } else {
  //         const error = result as ErrorResult;
  //         return Promise.reject(error);
  //       }
  //     })
  //     .catch(reason => {
  //       console.log(reason);
  //       return Promise.reject("Registration error");
  //     });
  // }

  checkUsername(username: string): Promise<CheckUsernameApiResult> {
    const body = {username: username};
    return this.http.post<CheckUsernameApiResult>(this.endpointUrl + "/account/checkUsername", body);
  }
}
