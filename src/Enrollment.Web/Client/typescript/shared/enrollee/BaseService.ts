import {Injectable} from "@angular/core"
import {Observable} from "rxjs/Observable";
import {Http} from "@angular/http";
import {contentHeaders} from "../requests/headers.const";
import {requestParams} from "../requests/params.method";

@Injectable()
export abstract class BaseService {
    constructor(
        protected http: Http
    ) {        
    }

    protected observableGet<T>(url: string): Observable<T> {
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

    protected observablePost<T>(url: string, params: any): Observable<T> {
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