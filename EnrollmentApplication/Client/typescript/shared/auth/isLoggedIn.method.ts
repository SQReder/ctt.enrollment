import {Injector} from 'angular2/core'
import {AuthService} from './auth.service';
import {appInjector} from '../../app.injector';
import {ComponentInstruction} from 'angular2/router';

export const isLoggedIn = (
    to: ComponentInstruction,
    from: ComponentInstruction
) => {
    const injector: Injector = appInjector();
    const auth: AuthService = injector.get(AuthService);
    return auth.check(to.urlPath);
};