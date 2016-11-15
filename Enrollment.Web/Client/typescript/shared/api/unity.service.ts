import {Injectable} from "@angular/core";
import {Http} from "@angular/http";
import {Observable} from "rxjs/Observable";

import {BaseApiService} from "./baseApi.service.class";

import {Unity, UnityGroup} from "../model/unity.class";

@Injectable()
export class UnityService extends BaseApiService {
    constructor(
        http: Http
    ) {
        super(http);
    }

    private buildUrl(unityGroupId: string = null, listUnities: boolean = false, unityId: string = null): string {
        let url = "/api/unityGroups";

        if (unityGroupId != null) url += `/${unityGroupId}`;
        if (unityGroupId != null && listUnities) url += "/unities";
        if (unityGroupId != null && unityId != null) url += `/${unityId}`;

        return url;
    }

    getUnityGroups(): Observable<UnityGroup[]> {
        const url = this.buildUrl();
        return this.get<UnityGroup[]>(url);
    }

    getUnityGroup(unityGroupId: string): Observable<UnityGroup> {
        const url = this.buildUrl(unityGroupId);
        return this.get<UnityGroup>(url);
    }

    getUnities(unityGroupId: string): Observable<Unity[]> {
        const url = this.buildUrl(unityGroupId, true);
        return this.get<Unity[]>(url);
    }

    getUnity(unityGroupId: string, unityId: string): Observable<Unity> {
        const url = this.buildUrl(unityGroupId, true, unityId);
        return this.get<Unity>(url);
    }
}
