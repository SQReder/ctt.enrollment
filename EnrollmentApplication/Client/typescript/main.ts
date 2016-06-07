import {provide, ComponentRef} from '@angular/core';
import {ROUTER_PROVIDERS} from '@angular/router-deprecated'
import {LocationStrategy, HashLocationStrategy} from '@angular/common'
import {HTTP_PROVIDERS} from '@angular/http';

import {bootstrap}    from '@angular/platform-browser-dynamic';

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
).then((appRef: ComponentRef<AppComponent>) => {
    // store a reference to the injector
    appInjector(appRef.injector);
});