import {Component, OnInit, Inject} from "@angular/core"
import {Router, ActivatedRoute, Params} from "@angular/router"
import {Location} from "@angular/common"
import {URLSearchParams} from "@angular/http"
import {EnrolleeService, Enrollee, RelationTypeEnum} from "./enrollee-service";
import {TrusteeService} from "../../services/api/trustee-service";
import {LocalStorage} from "../../services/local-storage";
import * as uuid from "uuid";

@Component({
  selector: "ctt-enrollee-edit",
  templateUrl: "enrollee-edit-component.html"
})
export class EnrolleeEditComponent implements OnInit {
  constructor(
    private enrolleeService: EnrolleeService,
    private trusteeService: TrusteeService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    @Inject(LocalStorage) private localStorage
  ) {
  }

  private pendingDataValue: boolean = true;
  model: Enrollee;
  trusteeAddress: string;

  get relations(): RelationTypeEnum[] {
    return [
      RelationTypeEnum.Child,
      RelationTypeEnum.Grandchild,
      RelationTypeEnum.Ward
    ]
  }

  ngOnInit(): void {
    this.pendingDataValue = true;

    this.loadTrusteeAddress();

    this.route.params.forEach((params: Params) => {
      let id = params["id"];

      let promise: Promise<Enrollee>;
      if (id !== undefined) {
        const params = new URLSearchParams();
        params.append("include", "address");
        promise = this.enrolleeService.read(id, params);
      } else {
        const enrollee = new Enrollee();
        promise = Promise.resolve(enrollee)
      }

      promise
        .then(enrollee => {
          this.model = enrollee;
          this.pendingDataValue = false
        });
    });
  }

  private loadTrusteeAddress() {
    const params = new URLSearchParams();
    params.append("include", "address");
    const trusteeId = this.localStorage["trusteeId"];

    this.trusteeService.read(trusteeId, params)
      .then(trustee => {
        this.trusteeAddress = trustee.address;
      });
  }

  get isDataLoaded(): boolean {
    return !this.pendingDataValue;
  }

  doSave(): Promise<never> {
    let promise: Promise<string>;

    const id = this.model.id;
    if (id == null){
      promise = this.enrolleeService.create(this.model)
        .then(guid => {
          this.router.navigateByUrl("/enrollee/edit/" + guid);
        })
    }else{
      promise = this.enrolleeService.update(id, this.model)
        .then(() => {});
    }

    return promise;
  }

  goBack() {
    this.location.back();
  }
}
