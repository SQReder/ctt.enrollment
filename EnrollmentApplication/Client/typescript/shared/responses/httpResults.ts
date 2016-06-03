import {IProfileInfo as ProfileInfo} from '../profile/profileInfo.interface';
import {ChildItemInterface}  from "../../components/profile/childItem.class";

export interface GenericResult {
    succeeded: boolean;
    message: string;
}

export interface ProfileInfoResult extends GenericResult {
    user: ProfileInfo
}

export interface ListChildrenResult extends GenericResult {
    children: ChildItemInterface[]
}