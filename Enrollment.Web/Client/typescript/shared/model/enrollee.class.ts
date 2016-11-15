import {RelationTypeEnum} from "./relationType.enum";

export class Enrollee {
    id: string;
    alternateId: number;

    relationType: RelationTypeEnum;

    firstName: string;
    middleName: string;
    lastName: string;

    address: string;
    addressSameAsParent: boolean;

    studyPlaceTitle: string;
    studyGrade: string;

    birthCertificateGuid: string; // optional
}