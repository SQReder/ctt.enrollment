import {ChildItem} from './childItem.class';
import {ProfileInfo} from './profileInfo.class';
import * as HttpResults from '../../shared/responses/httpResults';

export class Profile {
    info: ProfileInfo;
    children: ChildItem[];

    onProfileInfoLoaded(result: HttpResults.ProfileInfoResult) {
        console.log(result);
        this.info = result.user;
    }

    onListChildrenLoaded(result: HttpResults.ListChildrenResult) {
        console.log(result);
        this.children = result.children;
    }
}
