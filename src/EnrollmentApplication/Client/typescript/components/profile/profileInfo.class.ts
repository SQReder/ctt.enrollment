import {IProfileInfo} from '../../shared/profile/profileInfo.interface';

export class ProfileInfo implements IProfileInfo {
    firstName: string;
    lastName: string;
    middleName: string;

    job: string;
    jobPosition: string;

    phone: string;
    email: string;
    address: string;
}