import {Enrollee} from "../enrollee/enrollee.class";
import {TrusteeInfo} from "./TrusteeInfo.class";
import * as HttpResults from "../../shared/responses/httpResults";

export class Profile {
    info: TrusteeInfo;
    enrollees: Enrollee[];

    onProfileInfoLoaded(result: HttpResults.ITrusteeInfoResult) {
        console.log(result);
        this.info = result.trustee;
    }

    onListEnrolleeLoaded(result: HttpResults.IListEnrolleeResult) {
        console.log(result);
        this.enrollees = result.enrollees;
    }
}
