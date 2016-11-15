import {TypedHttpService} from "../http/typed-http";
import {AppOptions} from "../AppOptions";
import {Injectable} from "@angular/core";
import {CRUDService} from "./crud-service";

@Injectable()
export class TrusteeService extends CRUDService<TrusteeModel> {
  constructor(
    http: TypedHttpService,
    options: AppOptions,
  ) {
    super(http, options.apiEndpointUrl + "/trustee")
  }
}

export class TrusteeModel {
  id: string;
  firstName: string;
  lastName: string;
  middleName: string;
  sex: number;
  job: string;
  jobPosition: string;
  phoneNumber: string;
  email: string;
  address: string;
}
