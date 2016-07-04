import {Injectable} from '@angular/core'
import {Observable} from 'rxjs/Observable';
import {Http, Headers} from '@angular/http';
import {contentHeaders} from '../requests/headers.const';
import {requestParams} from '../requests/params.method';

@Injectable()
export class BaseService {
    constructor(
        protected http: Http
    ) {        
    }

    protected observableGet(url: string): Observable<any> {
        const headers = contentHeaders;
        return Observable.create(observer => {
            this.http
                .get(url, { headers: headers })
                .subscribe(responce => {
                    observer.next(responce.json());
                    observer.complete();
                });
        });        
    }

    protected observablePost(url: string, params: any): Observable<any> {
        const headers = contentHeaders;
        return Observable.create(observer => {
            this.http
                .post(url, params.toString(), { headers: headers })
                .subscribe(responce => {
                    observer.next(responce.json());
                    observer.complete();
                });
        });        
    }
}