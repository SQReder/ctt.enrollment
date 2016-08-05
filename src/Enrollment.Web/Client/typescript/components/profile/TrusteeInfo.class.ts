import {ITrusteeInfo} from "../../shared/profile/ITrusteeInfo.interface";

export class TrusteeInfo implements ITrusteeInfo {
    firstName: string;
    lastName: string;
    middleName: string;

    job: string;
    jobPosition: string;

    phoneNumber: string;
    email: string;
    address: string;
}