import {ITrusteeInfo} from "../profile/ITrusteeInfo.interface";
import {Enrollee}  from "../model/enrollee.class";
import {Trustee} from "../model/trustee.class";
import {Admission} from "../model/admission.class";

export interface IGenericResult {
    succeeded: boolean;
    message: string;
}

export interface ITrusteeResult extends IGenericResult {
    trustee: Trustee;
}

export interface IGetEnrolleeResult extends IGenericResult {
    enrollee: Enrollee;
}

export interface IGetEnrolleesResult extends IGenericResult {
    enrollees: Enrollee[];
}

export interface IGetAdmissionResult extends IGenericResult {
    admission: Admission;
}

export interface IGetAdmissionsResult extends IGenericResult {
    admissions: Admission[];
}


export interface IAddressResult extends IGenericResult {
    address: string;
}

export interface IGuidResult extends IGenericResult {
    guid: string;
}

export interface IErrorResult extends  IGenericResult {
    errorType: string;
    baseMessage: string;
}
