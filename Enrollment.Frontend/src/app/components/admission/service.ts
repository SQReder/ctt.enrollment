import {Injectable} from "@angular/core";
import {TypedHttpService} from "../../services/http/typed-http";
import {AppOptions} from "../../services/AppOptions";
import {CRUDService} from "../../services/api/crud-service";
import {Admission} from "./admission-class";

@Injectable()
export class AdmissionService extends CRUDService<Admission> {
  private apiEndpoint: string;

  constructor(
    http: TypedHttpService,
    appOptions: AppOptions
  ) {
    super(http, appOptions.apiEndpointUrl + "/admission");
  }
}
