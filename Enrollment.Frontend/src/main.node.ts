import {NgModule} from "@angular/core";
import {FormsModule} from "@angular/forms";
import {UniversalModule} from "angular2-universal";
import {App} from "./app/app";
import {DashboardComponent} from "./app/components/dashboard/dashboard.component";
import {routing} from "./app/app-routing";
import {TypedHttpService, TypedHttpServiceWithXsrfProtection} from "./app/services/http/typed-http";
import {WhenLoggedInDirective} from "./app/services/auth/when-logged-in-directive";
import {WhenNotLoggedInDirective} from "./app/services/auth/when-not-logged-in-directive";
import {AuthService} from "./app/services/auth/auth-service";
import {LoginPanel} from "./app/components/login/login-panel";
import {LoginComponent} from "./app/components/login/login-component";
import {XSRFStrategy, CookieXSRFStrategy} from "@angular/http";
import {AppOptions} from "./app/services/AppOptions";
import {RegistrationComponent} from "./app/components/registration/registration-component";
import {ProfileComponent} from "./app/components/profile/profile-component";
import {TextMaskModule} from "angular2-text-mask";
import {TrusteeService} from "./app/services/api/trustee-service";
import {LocalStorage, DummyLocalStorage} from "./app/services/local-storage";
import {Cookie, CookieNode} from "./app/services/cookie";
import {WhenInRoleDirective} from "./app/services/auth/when-in-role-directive";
import {ProfileService} from "./app/services/api/profile-service";
import {EnrolleeService} from "./app/components/enrollee/enrollee-service";
import {EnrolleeModule} from "./app/components/enrollee/enrollee-module";
import {AdmissionModule} from "./app/components/admission/module";

@NgModule({
  bootstrap: [App],
  declarations: [
    App,
    DashboardComponent,
    WhenLoggedInDirective,
    WhenNotLoggedInDirective,
    WhenInRoleDirective,
    LoginPanel,
    LoginComponent,
    RegistrationComponent,
    ProfileComponent,
  ],
  providers: [
    AppOptions,
    AuthService,
    TrusteeService,
    ProfileService,
    EnrolleeService,
    {provide: Cookie, useValue: new CookieNode()},
    {provide: TypedHttpService, useClass: TypedHttpServiceWithXsrfProtection},
    {provide: XSRFStrategy, useValue: new CookieXSRFStrategy()},
    {provide: LocalStorage, useValue: DummyLocalStorage},
  ],
  imports: [
    UniversalModule, // NodeModule, NodeHttpModule, and NodeJsonpModule are included
    FormsModule,
    TextMaskModule,
    routing,
    EnrolleeModule,
    AdmissionModule,
  ]
})
export class MainModule {
}
