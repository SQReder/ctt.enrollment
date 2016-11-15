import {Observable} from "rxjs/Observable";
import "../rxjs-operators";
import {Http, Response, Headers, RequestOptionsArgs} from "@angular/http";
import {Injectable} from "@angular/core";
import {Cookie} from "../cookie";

@Injectable()
export class TypedHttpService {
  constructor(private http: Http) {
  }

  private setHeaders(options: RequestOptionsArgs) {
    if (!options.headers) {
      options.headers = new Headers();
    }

    options.headers.set("Content-Type", "application/json")
  }

  get<T>(url: string, options: RequestOptionsArgs = {}): Promise<T> {
    this.setHeaders(options);
    return this.processResponse<T>(this.http.get(url, options));
  }

  post<T>(url: string, body: any, options: RequestOptionsArgs = {}): Promise<T> {
    this.setHeaders(options);
    const encodedBody = JSON.stringify(body);
    return this.processResponse<T>(this.http.post(url, encodedBody, options));
  }

  update<T>(url: string, body: any, options: RequestOptionsArgs = {}): Promise<T> {
    this.setHeaders(options);
    const encodedBody = JSON.stringify(body);
    return this.processResponse<T>(this.http.put(url, encodedBody, options));
  }

  delete<T>(url: string, options: RequestOptionsArgs = {}): Promise<T> {
    this.setHeaders(options);
    return this.processResponse<T>(this.http.delete(url, options));
  }

  private processResponse<T>(response: Observable<Response>): Promise<T> {
    //noinspection TypeScriptUnresolvedFunction
    return response
      .toPromise()
      .then(res => this.extractData<T>(res))
      .catch(this.handleError);
  }

  private extractData<T>(res: Response): T {
    return res.json();
  }

  protected handleError(error: any): Promise<any> {
    // ToDo implement logging or base error handling
    console.error(error);

    return Promise.reject(error);
  }
}

@Injectable()
export class TypedHttpServiceWithXsrfProtection extends TypedHttpService {
  private xsrfCookieName = "XSRF-TOKEN";
  private xsrfTokenUpdateUrl: string = "//localhost:5000/api/token/update";

  constructor(
    http: Http,
    private cookie: Cookie
  ) {
    super(http);
  }


  private updateXsrfToken(ignoreStoredToken: boolean = false): Promise<any> {
    const token = this.cookie.get(this.xsrfCookieName);
    if (!ignoreStoredToken && token)
      return Promise.resolve();

    return super.get<any>(this.xsrfTokenUpdateUrl, {withCredentials: true});
  }

  private executeRequest<T>(requestAction: ()=>Promise<T>) {
    return this.updateXsrfToken()
      .then(requestAction)
      .then((json: any) => {
        if (json.succeeded === false && json.errorCode === "BadCsrfToken") {
          return this
            .updateXsrfToken(true)
            .then(requestAction);
        } else {
          return Promise.resolve(json);
        }
      });
  }

  /* BaseApiService overrides */

  get<T>(url: string, options: RequestOptionsArgs = {}): Promise<T> {
    options.withCredentials = true;
    return this.executeRequest(() => {
      return super.get(url, options);
    });
  }

  post<T>(url: string, body: string, options: RequestOptionsArgs = {}): Promise<T> {
    options.withCredentials = true;
    return this.executeRequest((): Promise<T> => {
      return super.post(url, body, options);
    });
  }

  update<T>(url: string, body: string, options: RequestOptionsArgs = {}): Promise<T> {
    options.withCredentials = true;
    return this.executeRequest<T>(() => {
      return super.update(url, body, options);
    });
  }

  delete<T>(url: string, options: RequestOptionsArgs = {}): Promise<T> {
    options.withCredentials = true;
    return this.executeRequest(() => {
      return super.delete(url, options);
    });
  }
}
