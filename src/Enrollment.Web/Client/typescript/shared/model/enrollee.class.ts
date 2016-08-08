import {RelationTypeEnum} from "./relationType.enum";

export class Enrollee {
    id: string;
    relationType: RelationTypeEnum;
    firstName: string;
    middleName: string;
    lastName: string;
    address: string;
    addressSameAsParent: boolean;
    studyPlaceTitle: string;
    studyGrade: string;
    birthCertificateGuid: string; // optional

    public x(): string {
        return "di";
    }

    get fullName(): string {
        //        const middleNamePart = this.middleName != null ? ` ${this.middleName}` : "";
        return `${this.lastName} ${this.firstName}`;// + middleNamePart;
    }
}