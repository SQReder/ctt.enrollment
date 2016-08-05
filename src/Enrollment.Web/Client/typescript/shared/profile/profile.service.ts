import {Injectable} from "@angular/core"
import {Observable} from "rxjs/Observable";
import {Router, RouterLink, ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {Http, Headers} from "@angular/http";
import {contentHeaders} from "../requests/headers.const";
import {requestParams} from "../requests/params.method";
import * as HttpResults from "../responses/httpResults";
import {BaseService} from "../enrollee/BaseService";

@Injectable()
export class ProfileService extends BaseService {
    constructor(
        http: Http
    ) {
        super(http);
    }
}