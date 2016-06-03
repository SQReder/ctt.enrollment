'use strict';

import {Injectable, Component, EventEmitter} from 'angular2/core';
import {URLSearchParams} from 'angular2/http';
import {Observable} from 'rxjs/Observable';

import { Router, RouterLink, ROUTER_PROVIDERS} from 'angular2/router';
import { Http, Headers } from 'angular2/http';
import { contentHeaders } from '../requests/headers.const';
import { requestParams } from '../requests/params.method';

import {User} from './user.class';
import {RegistrationModel} from "./registrationModel.class";
import {GenericResult} from "../responses/httpResults";

@Component({
    providers: [
        ROUTER_PROVIDERS
    ]
})

@Injectable()
export class AuthService {
    private tokenName = 'user';
    token: string;

    onIsLoggedInChanged = new EventEmitter<boolean>();

    constructor(
        public router: Router,
        public http: Http
    ) {
    }

    check(returnUrl?: string) {
        if (!localStorage.getItem(this.tokenName)) {
            if (returnUrl != null) {
                this.router.navigate(['Login', { "returnUrl": returnUrl }]);
            } else {
                this.router.navigate(['Login']);
            }
            return false;
        } else {
            return true;
        }
    }

    login(username: string, password: string, returnUrl: string) {
        returnUrl = returnUrl == null ? '/' : returnUrl;
        const headers = contentHeaders;

        const params = requestParams();
        params.set('username', username);
        params.set('password', password);
        params.set('rememberMe', true.toString());

        this.http.post('./Account/Login', params.toString(), { headers: headers }).subscribe(
            response => {
                var loginResult = response.json();
                if (loginResult.succeeded) {
                    const userToken = JSON.stringify(new User(username, loginResult.roles));
                    localStorage.setItem(this.tokenName, userToken);

                    this.onIsLoggedInChanged.emit(true);
                    this.router.navigateByUrl(returnUrl);
                }
            }
        );
    }

    logout() {
        const headers = contentHeaders;
        const params = requestParams();

        this.http.post('./Account/Logout', params.toString(), { headers: headers }).subscribe(
            response => {
                var logoutResult = response.json();
                if (logoutResult.succeeded) {
                    this.onIsLoggedInChanged.emit(false);
                    localStorage.removeItem(this.tokenName);
                    this.router.navigate(['Login']);
                }
            }
        );
    }

    register(model: RegistrationModel): Observable<GenericResult> {
        const headers = contentHeaders;
        const params = requestParams();
        for (let key in model) {
            if (model.hasOwnProperty(key)) {
                params.set(key, model[key]);
            }            
        }

        return Observable.create(observer => {
            this.http
                .post('./Account/Registration', params.toString(), { headers: headers })
                .subscribe(responce => {
                    observer.next(responce.json());
                    observer.complete();
                });
        });
    }
}