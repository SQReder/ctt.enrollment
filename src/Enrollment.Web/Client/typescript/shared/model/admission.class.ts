import {Enrollee} from "./enrollee.class";
import {Unity} from "./unity.class";

export class Admission {
    id: string;
    alternateId: string;
    enrolleeId: string;
    unityId: string;

    enrollee: Enrollee;
    unity: Unity;
}