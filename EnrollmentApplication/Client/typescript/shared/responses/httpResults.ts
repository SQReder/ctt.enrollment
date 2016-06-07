import {IProfileInfo as ProfileInfo} from '../profile/profileInfo.interface';
import {EnrolleeItemInterface}  from "../../components/enrollee/enrolleeItem.class";

export interface GenericResult {
    succeeded: boolean;
    message: string;
}

export interface ProfileInfoResult extends GenericResult {
    user: ProfileInfo
}

export interface ListEnrolleeResult extends GenericResult {
    children: EnrolleeItemInterface[]
}