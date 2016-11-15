import {Injectable, OnInit} from "@angular/core";
import {RouteParams, Router, ROUTER_DIRECTIVES} from "@angular/router-deprecated";

import {ITrusteeResult, IErrorResult, IGenericResult} from "../../../../shared/responses/httpResults";

import {TrusteeService} from "../../../../shared/api/trustee.service";
import {Trustee} from "../../../../shared/model/trustee.class";
import {BaseViewModel} from "../../../../shared/views/baseView.class";
import {Notifier} from "../../../../shared/uikit/notify.service";

@Injectable()
export class TrusteeEditModel extends BaseViewModel {
    trustee: Trustee;

    constructor(
        notifier: Notifier,
        private trusteeService: TrusteeService
    ) {
        super(notifier);        
    }

    initialize(): void {
        this.trusteeService
            .getCurrent()
            .subscribe(this.onLoadTrustee);
    }

    onLoadTrustee = (result: ITrusteeResult | IErrorResult) => {
        if (result.succeeded) {
            const trusteeResult = result as ITrusteeResult;
            this.trustee = trusteeResult.trustee;
        } else {
            this.requestErrorHandler(result as IErrorResult);
        }
    }
}