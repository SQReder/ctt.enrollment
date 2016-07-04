import {Injector} from '@angular/core'
import {AuthService} from './auth.service';
import {appInjector} from '../../app.injector';
import {ComponentInstruction} from '@angular/router-deprecated';

export const isLoggedIn = (
    to: ComponentInstruction,
    from: ComponentInstruction
) => {
    const injector: Injector = appInjector();
    const auth: AuthService = injector.get(AuthService);
    return auth.check(to.urlPath);
};