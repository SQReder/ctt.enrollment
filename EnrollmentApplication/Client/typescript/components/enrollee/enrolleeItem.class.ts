export enum RelationshipDegreeEnum {
    Child = 0, // Сын/дочь
    Grandchild = 1, // Внук/внучка
    Ward = 2, // Опекаемый
}

export interface EnrolleeItemInterface {
    id: string;
    relationshipDegree: RelationshipDegreeEnum;
    firstName: string;
    middleName: string;
    lastName: string;
    address: string;
    addressSameAsParent: boolean;
    studyPlaceTitle: string;
    studyGrade: string;
    birthCertificateGuid: string; // optional
}

export class EnrolleeItem implements EnrolleeItemInterface {
    id: string;
    relationshipDegree: RelationshipDegreeEnum;
    firstName: string;
    middleName: string;
    lastName: string;
    address: string;
    addressSameAsParent: boolean;
    studyPlaceTitle: string;
    studyGrade: string;
    birthCertificateGuid: string; // optional
}