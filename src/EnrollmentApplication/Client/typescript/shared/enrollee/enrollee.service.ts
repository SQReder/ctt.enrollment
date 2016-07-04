import {Injectable} from '@angular/core'
import {Observable} from 'rxjs/Observable';
import {Router, RouterLink, ROUTER_PROVIDERS} from '@angular/router-deprecated';
import {Http, Headers} from '@angular/http';
import {contentHeaders} from '../requests/headers.const';
import {requestParams} from '../requests/params.method';
import * as HttpResults from '../responses/httpResults';
import {Enrollee} from '../../components/enrollee/enrollee.class';
import * as Service from './BaseService';

@Injectable()
export class EnrolleeService extends Service.BaseService {
    constructor(
        protected http: Http
    ) {
        super(http);
    }

    listEnrollee(): Observable<HttpResults.ListEnrolleeResult> {
        return this.observableGet('/Enrollee/ListEnrollee');
    }

    getEnrollee(id: string): Observable<HttpResults.GetEnrolleeResult> {
        if (id == null) {
            id = '';
        }

        return this.observableGet('/Enrollee/Get/' + id);
    }

    saveEnrollee(model: Enrollee): void {
        const params = requestParams();
        params.set('model', JSON.stringify(model));

        this.observablePost('/Enrollee/Edit', params);
    }
}