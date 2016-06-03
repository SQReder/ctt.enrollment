/// <reference path="../../node_modules/angular2/typings/browser.d.ts" />
import {provide, ComponentRef} from 'angular2/core';
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy} from 'angular2/router'
import {HTTP_PROVIDERS} from 'angular2/http';

import {bootstrap}    from 'angular2/platform/browser';

import {AppComponent} from './components/app/app.component';
import {appInjector} from './app.injector';
import {AuthService} from './shared/auth/auth.service';
import {ProfileService} from './shared/profile/profile.service';

declare var appOptions: any;

bootstrap(
    AppComponent, 
    [
        AuthService,
        ProfileService,
        ROUTER_PROVIDERS,
        HTTP_PROVIDERS,        
    ]
).then((appRef: ComponentRef) => {
    // store a reference to the injector
    appInjector(appRef.injector);
});