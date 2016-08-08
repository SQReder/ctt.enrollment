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
        return this.http.get(url)
            .map<T>(this.extractData)
            .catch(this.handleError);
    }

    protected post<T>(url: string, body: any): Observable<T> {
        return this.http.post(url, body)
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
