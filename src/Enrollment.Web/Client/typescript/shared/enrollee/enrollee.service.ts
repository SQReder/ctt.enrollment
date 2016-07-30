import {Injectable} from "@angular/core"
import {Observable} from "rxjs/Observable";
import {Router, RouterLink, ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {Http, Headers} from "@angular/http";
import {contentHeaders} from "../requests/headers.const";
import {requestParams} from "../requests/params.method";
import * as HttpResults from "../responses/httpResults";
import {Enrollee} from "../../components/enrollee/enrollee.class";
import * as Service from "./BaseService";

@Injectable()
export class EnrolleeService extends Service.BaseService {
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
        function capitalizeFirstLetter(string: string): string {
            return string.charAt(0).toUpperCase() + string.slice(1);
        }

        const params = requestParams();
        //params.set("model", JSON.stringify(model));

        for (let key in model) {
            if (!model.hasOwnProperty(key))
                continue;

            params.set(key, model[key]);
        }

        return this.observablePost<HttpResults.IGuidResult>("/Enrollee/Edit", params);
    }
}