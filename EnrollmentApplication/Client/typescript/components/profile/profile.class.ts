import {EnrolleeItem} from '../enrollee/enrolleeItem.class';
import {ProfileInfo} from './profileInfo.class';
import * as HttpResults from '../../shared/responses/httpResults';

export class Profile {
    info: ProfileInfo;
    children: EnrolleeItem[];

    onProfileInfoLoaded(result: HttpResults.ProfileInfoResult) {
        console.log(result);
        this.info = result.user;
    }

    onListEnrolleeLoaded(result: HttpResults.ListEnrolleeResult) {
        console.log(result);
        this.children = result.children;
    }
}
