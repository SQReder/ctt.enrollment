import {Injectable, Inject} from "@angular/core"
import {LocalStorage} from "./local-storage";

@Injectable()
export class StorageModel {
  constructor(
    @Inject(LocalStorage) private localStorage
  ) {
  }
}
