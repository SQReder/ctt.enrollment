import {Observable} from "rxjs/Observable";
import "./../rxjs-operators";
import {Http, Response, Headers} from "@angular/http";

export abstract class BaseApiService {
    private contentHeaders: Headers;

    constructor(
        private http: Http
    ) {
        this.contentHeaders = new Headers();
        this.contentHeaders.append("Content-Type", "application/json;charset=utf-8");
    }

    protected get<T>(url: string): Observable<T> {
        return this.processResponce(this.http.get(url));
    }

    protected post<T>(url: string, body: any): Observable<T> {
        return this.processResponce(this.http.post(url, body));
    }

    protected update<T>(url: string, body: any): Observable<T> {
        return this.processResponce(this.http.put(url, body));
    }

    protected delete<T>(url: string): Observable<T> {
        return this.processResponce(this.http.delete(url));
    }

    private processResponce<T>(responce: Observable<Response>): Observable<T> {
        return responce
            .map<T>(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        const body = res.json();
        return body || {};
    }

    private handleError(error: any) {
        const errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : "Server error";
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
