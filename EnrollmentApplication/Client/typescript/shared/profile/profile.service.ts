import {Injectable} from '@angular/core'
import {Observable} from 'rxjs/Observable';
import {Router, RouterLink, ROUTER_PROVIDERS} from '@angular/router-deprecated';
import {Http, Headers} from '@angular/http';
import {contentHeaders} from '../requests/headers.const';
import {requestParams} from '../requests/params.method';
import * as HttpResults from "../responses/httpResults";

@Injectable()
export class ProfileService {
    constructor(
        private http: Http
    ) {        
    }    

    getCurrentUser(): Observable<HttpResults.ProfileInfoResult> {
        const headers = contentHeaders;
        return Observable.create(observer => {
            this.http
                .get('/Profile/GetCurrentUser', { headers: headers })
                .subscribe(responce => {
                    observer.next(responce.json());
                    observer.complete();
                });
        });
    }

    listChildren(): Observable<HttpResults.ListEnrolleeResult> {
        const headers = contentHeaders;
        return Observable.create(observer => {
            this.http
                .get('/Profile/ListChildren', { headers: headers })
                .subscribe(responce => {
                    observer.next(responce.json());
                    observer.complete();
                });
        });
    }
}