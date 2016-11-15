/// <reference path="../../typings/index.d.ts"/>
import {provide, ComponentRef} from "@angular/core";
import {ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {LocationStrategy, HashLocationStrategy} from "@angular/common";
import {HTTP_PROVIDERS} from "@angular/http";
import {disableDeprecatedForms, provideForms} from "@angular/forms";

import {bootstrap}    from "@angular/platform-browser-dynamic";

import {TrusteeAppComponent} from "./apps/trustee/trustee.app.component";
import {appInjector} from "./app.injector";
import {AuthService} from "./shared/auth/auth.service";

import {GuidGeneratorService} from "./shared/guidGenerator/guidGenerator.service";
import {UnityService} from "./shared/api/unity.service";
import {TrusteeService} from "./shared/api/trustee.service";
import {RandomIdGenerator} from "./shared/randomIdGenerator/randomIdGenerator.service";
import {Notifier, UIkitNotifier} from "./shared/uikit/notify.service";
import {ModalService, UIkitModalService} from "./shared/uikit/modal.service";

declare var appOptions: any;

bootstrap(
    TrusteeAppComponent,
    [
        AuthService, 
        TrusteeService,
        UnityService,
        ROUTER_PROVIDERS,
        HTTP_PROVIDERS,
        disableDeprecatedForms(),
        provideForms(),
        GuidGeneratorService,
        provide(Notifier, { useValue: new UIkitNotifier() }),
        provide(ModalService, {useValue: new UIkitModalService() }),
        provide(RandomIdGenerator, {useValue: new RandomIdGenerator(999999)})
    ]
).then((appRef: ComponentRef<TrusteeAppComponent>) => {
    // store a reference to the injector
    appInjector(appRef.injector);
}).catch((err: any) => console.error(err));