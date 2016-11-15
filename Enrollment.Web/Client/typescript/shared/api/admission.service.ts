import {Injectable} from "@angular/core";
import {Http} from "@angular/http";
import {Observable} from "rxjs/Observable";

import {BaseApiService} from "./baseApi.service.class";

export class AdmissionService extends BaseApiService {
    constructor(
        http: Http
    ) {
        super(http);
    }
}