import {IProfileInfo as ProfileInfo} from '../profile/profileInfo.interface';
import {IEnrollee}  from '../../components/enrollee/enrollee.class';

export interface GenericResult {
    succeeded: boolean;
    message: string;
}

export interface ProfileInfoResult extends GenericResult {
    user: ProfileInfo
}

export interface ListEnrolleeResult extends GenericResult {
    enrollees: IEnrollee[]
}

export interface GetEnrolleeResult extends GenericResult {
    enrollee: IEnrollee
}