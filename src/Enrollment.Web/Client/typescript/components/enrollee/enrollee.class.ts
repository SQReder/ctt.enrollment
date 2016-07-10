import {RelationTypeEnum} from "./relationType.enum";

export interface IAddress {
    raw: string;
}

export class Address implements IAddress
{
    raw: string;
}

export interface IEnrollee {
    id: string;
    relationType: RelationTypeEnum;
    firstName: string;
    middleName: string;
    lastName: string;
    address: IAddress;
    addressSameAsParent: boolean;
    studyPlaceTitle: string;
    studyGrade: string;
    birthCertificateGuid: string; // optional
}

export class Enrollee implements IEnrollee {
    id: string;
    relationType: RelationTypeEnum;
    firstName: string;
    middleName: string;
    lastName: string;
    address: IAddress;
    addressSameAsParent: boolean;
    studyPlaceTitle: string;
    studyGrade: string;
    birthCertificateGuid: string; // optional

    constructor() {
        this.address = new Address();
    }
}