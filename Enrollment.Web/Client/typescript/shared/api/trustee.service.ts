import {Injectable} from "@angular/core";
import {Http} from "@angular/http";
import {Observable} from "rxjs/Observable";

import {BaseApiService} from "./baseApi.service.class";

import {Trustee} from "../model/trustee.class";
import {Enrollee} from "../model/enrollee.class";
import {Admission} from "../model/admission.class";

import {
    IGuidResult,
    IErrorResult,
    IGetAdmissionResult,
    IGetAdmissionsResult,
    IAddressResult,
    IGenericResult,
    ITrusteeResult,
    IGetEnrolleesResult,
    IGetEnrolleeResult
} from "../responses/httpResults";

@Injectable()
export class TrusteeService extends BaseApiService
{
    constructor(
        http: Http
    ) {
        super(http);
    }

    getCurrent(): Observable<ITrusteeResult | IErrorResult> {
        return this.get<ITrusteeResult>("/api/me");
    }

    getAddress(): Observable<string> {
        // ToDo Handle errors
        return this.getCurrent()
            .map<string>((result: ITrusteeResult) => result.trustee.address);
    } 


    /* enrollee methods */

    // List
    getEnrollees(): Observable<IGetEnrolleesResult | IErrorResult> {
        return this.get<IGetEnrolleesResult | IErrorResult>("/api/me/enrollees");
    }

    // Create
    createEnrollee(enrollee: Enrollee): Observable<IGuidResult | IErrorResult> {
        return this.post<IGuidResult | IErrorResult>("/api/me/enrollees/", enrollee);
    }

    // Read
    getEnrollee(id: string): Observable<IGetEnrolleeResult | IErrorResult> {
        return this.get<IGetEnrolleeResult | IErrorResult>(`/api/me/enrollees/${id}`);
    }

    // Update
    updateEnrollee(enrollee: Enrollee): Observable<IGenericResult | IErrorResult> {
        throw new Error("Not implemented");
    }

    // Delete
    deleteEnrollee(id: string): Observable<IGenericResult | IErrorResult> {
        return this.delete<IGenericResult | IErrorResult>(`/api/me/enrollees/${id}`);
    }


    /* Admission methods */

    getAdmissions(): Observable<IGetAdmissionsResult | IErrorResult> {
        return this.get<IGetAdmissionsResult | IErrorResult>("/api/me/admissions");
    }

    getAdmission(id: string): Observable<IGetAdmissionResult | IErrorResult> {
        throw new Error("Not implemented");
    }

    createAdmission(admission: Admission): Observable<IGenericResult | IErrorResult> {
        return this.post<IGenericResult | IErrorResult>("/api/me/admissions", admission);
    }

    updateAdmission(model: Admission): Observable<IGenericResult | IErrorResult> {
        throw new Error("Not implemented");
    }

    deleteAdmission(id: string): Observable<IGenericResult | IErrorResult> {
        return this.delete<IGenericResult | IErrorResult>(`/api/me/admissions/${id}`);
    }

    downloadAdmission(id: string): Observable<IErrorResult> {
        return this.get<IErrorResult>(`/api/me/admissions/${id}/download`);
    }
}