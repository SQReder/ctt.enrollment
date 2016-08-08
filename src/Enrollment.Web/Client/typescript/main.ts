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

import {GuidGeneratorService} from "./shared/guidGenerator/guidGenerator.service";
import {DeprecatedEnrolleeService} from "./shared/enrollee/enrollee.service";
import {BaseService} from "./shared/enrollee/BaseService";
import {DeprecatedTrusteeService} from "./shared/trustee/trustee.service";
import {UnityService} from "./shared/api/unity.service";
import {TrusteeService} from "./shared/api/trustee.service";
import {RandomIdGenerator} from "./shared/randomIdGenerator/randomIdGenerator.service";

declare var appOptions: any;

bootstrap(
    AppComponent,
    [
        BaseService,
        AuthService, 
        DeprecatedTrusteeService,
        DeprecatedEnrolleeService,
        TrusteeService,
        UnityService,
        ROUTER_PROVIDERS,
        HTTP_PROVIDERS,
        disableDeprecatedForms(),
        provideForms(),
        GuidGeneratorService,
        provide(RandomIdGenerator, {useValue: new RandomIdGenerator(999999)})
    ]
).then((appRef: ComponentRef<AppComponent>) => {
    // store a reference to the injector
    appInjector(appRef.injector);
}).catch((err: any) => console.error(err));