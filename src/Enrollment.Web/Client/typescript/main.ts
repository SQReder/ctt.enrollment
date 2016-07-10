/// <reference path="../../typings/main.d.ts"/>
import {provide, ComponentRef} from "@angular/core";
import {ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {LocationStrategy, HashLocationStrategy} from "@angular/common";
import {HTTP_PROVIDERS} from "@angular/http";
import {disableDeprecatedForms, provideForms} from "@angular/forms";

import {bootstrap}    from "@angular/platform-browser-dynamic";

import {AppComponent} from "./components/app/app.component";
import {appInjector} from "./app.injector";
import {AuthService} from "./shared/auth/auth.service";

import {ProfileService} from "./shared/profile/profile.service";
import {EnrolleeService} from "./shared/enrollee/enrollee.service";
import {BaseService} from "./shared/enrollee/BaseService";

declare var appOptions: any;

bootstrap(
    AppComponent,
    [
        BaseService,
        AuthService, 
        ProfileService,
        EnrolleeService,
        ROUTER_PROVIDERS,
        HTTP_PROVIDERS,
        disableDeprecatedForms(),
        provideForms()
    ]
).then((appRef: ComponentRef<AppComponent>) => {
    // store a reference to the injector
    appInjector(appRef.injector);
}).catch((err: any) => console.error(err));