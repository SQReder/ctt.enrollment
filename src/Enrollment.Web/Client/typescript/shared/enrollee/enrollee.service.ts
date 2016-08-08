import {Injectable} from "@angular/core"
import {Observable} from "rxjs/Observable";
import {Router, RouterLink, ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {Http, Headers} from "@angular/http";
import {contentHeaders} from "../requests/headers.const";
import {requestParams} from "../requests/params.method";
import * as HttpResults from "../responses/httpResults";
import {DeprecatedEnrollee as Enrollee} from "../../components/enrollee/enrollee.class";
import * as Service from "./BaseService";

@Injectable()
export class DeprecatedEnrolleeService extends Service.BaseService {
    constructor(
        protected http: Http
    ) {
        super(http);
    }

    listEnrollee(): Observable<HttpResults.IListEnrolleeResult> {
        return this.observableGet("/Enrollee/ListEnrollee");
    }

    getEnrollee(id: string): Observable<HttpResults.IGetEnrolleeResult> {
        if (id == null) {
            id = "";
        }

        return this.observableGet <HttpResults.IGetEnrolleeResult>(`/Enrollee/Get/${id}`);
    }

    saveEnrollee(model: Enrollee): Observable<HttpResults.IGuidResult> {
        const params = requestParams();

        for (let key in model) {
            if (!model.hasOwnProperty(key))
                continue;

            params.set(key, model[key]);
        }

        return this.observablePost<HttpResults.IGuidResult>("/Enrollee/Edit", params);
    }

    deleteEnrollee(id: string): Observable<HttpResults.IGenericResult> {
        const params = requestParams();

        params.set("id", id);

        return this.observablePost<HttpResults.IGenericResult>("/Enrollee/Delete", params);
    }
}