import {ITrusteeInfo} from "../profile/ITrusteeInfo.interface";
import {IEnrollee, IAddress }  from "../../components/enrollee/enrollee.class";

export interface IGenericResult {
    succeeded: boolean;
    message: string;
}

export interface ITrusteeInfoResult extends IGenericResult {
    trustee: ITrusteeInfo
}

export interface IListEnrolleeResult extends IGenericResult {
    enrollees: IEnrollee[]
}

export interface IGetEnrolleeResult extends IGenericResult {
    enrollee: IEnrollee
}

export interface ITrusteeAddressResult extends IGenericResult {
    address: IAddress
}