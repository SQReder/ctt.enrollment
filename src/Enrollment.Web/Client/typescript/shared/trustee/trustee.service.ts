import {Http} from "@angular/http";
import * as HttpResults from "../responses/httpResults"
import {Observable} from "rxjs/Observable";
import {BaseService} from "../enrollee/BaseService";

export class TrusteeService extends BaseService {
    constructor(
        http: Http
    ) {
        super(http);
    }

    getCurrentTrusteeAddress(): Observable<HttpResults.ITrusteeAddressResult> {
        return this.observableGet<HttpResults.ITrusteeAddressResult>("/Trustee/GetCurrentTrusteeAddress");
    }

    getCurrentTrustee(): Observable<HttpResults.ITrusteeInfoResult> {
        return this.observableGet<HttpResults.ITrusteeInfoResult>("/Trustee/GetCurrentTrustee");
    }
}