import {Injectable, Inject} from "@angular/core";
import {TypedHttpService} from "../http/typed-http";
import {LocalStorage} from "../local-storage";
import {AppOptions} from "../AppOptions";
import {CRUDService} from "./crud-service";

@Injectable()
export class ProfileService extends CRUDService<ProfileInfo> {
  private apiEndpoint: string;

  constructor(
    http: TypedHttpService,
    appOptions: AppOptions
  ) {
    super(http, appOptions.apiEndpointUrl + "/profile");
  }
}

export interface ProfileInfo {
  id: string;
  trusteeId: string;
  email: string;
}
