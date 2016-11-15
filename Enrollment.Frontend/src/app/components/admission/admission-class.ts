import {Unity} from "../unities/unity.class";
import {Enrollee} from "../enrollee/enrollee-service";

export class Admission {
    id: string;
    alternateId: string;

    enrolleeId: string;
    unityId: string;

    enrollee: Enrollee;
    unity: Unity;
}
