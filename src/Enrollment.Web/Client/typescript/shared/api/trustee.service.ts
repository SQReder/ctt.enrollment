import {Injectable} from "@angular/core";
import {Http} from "@angular/http";
import {Observable} from "rxjs/Observable";

import {BaseApiService} from "./baseApiService.class";

import {Trustee} from "../model/trustee.class";
import {Enrollee} from "../model/enrollee.class";
import {Admission} from "../model/admission.class";
import * as HttpResults from "../responses/httpResults";

@Injectable()
export class TrusteeService extends BaseApiService
{
    constructor(
        http: Http
    ) {
        super(http);
    }

    getCurrentTrustee(): Observable<Trustee> {
        return this.get<Trustee>("/api/me");
    }

    getCurrentTrusteeEnrollees(): Observable<Enrollee[]> {
        return this.get<Enrollee[]>("/api/me/enrollees");
    }

    getCurrentTrusreeAdmissions(): Observable<Admission[]> {
        return this.get<Admission[]>("/api/me/admissions");
    }

    create(admission: Admission): Observable<HttpResults.IGenericResult> {
        return this.post<HttpResults.IGenericResult>("/api/me/admissions", admission);
    }
}