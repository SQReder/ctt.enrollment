import {Injectable} from 'angular2/core'
import {Observable} from 'rxjs/Observable';
import {Router, RouterLink, ROUTER_PROVIDERS} from 'angular2/router';
import {Http, Headers} from 'angular2/http';
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

    listChildren(): Observable<HttpResults.ListChildrenResult> {
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