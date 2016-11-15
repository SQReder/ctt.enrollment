import {Injectable} from "@angular/core";
import {TypedHttpService} from "../../services/http/typed-http";
import {AppOptions} from "../../services/AppOptions";
import {CRUDService} from "../../services/api/crud-service";

@Injectable()
export class EnrolleeService extends CRUDService<Enrollee> {
  private apiEndpoint: string;

  constructor(
    http: TypedHttpService,
    appOptions: AppOptions
  ) {
    super(http, appOptions.apiEndpointUrl + "/enrollee");
  }
}

export enum RelationTypeEnum {
  Child = 0, // Сын/Дочь
  Grandchild = 1, // Внук/Внучка
  Ward = 2, // Подопечный
}

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
