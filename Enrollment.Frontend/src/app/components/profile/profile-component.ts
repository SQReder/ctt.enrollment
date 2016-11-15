import {Component, OnInit, Inject} from "@angular/core"
import {AuthService} from "../../services/auth/auth-service";
import {Router} from "@angular/router";
import {TrusteeService, TrusteeModel} from "../../services/api/trustee-service";
import {ProfileService} from "../../services/api/profile-service";
import {URLSearchParams} from "@angular/http"
import {LocalStorage} from "../../services/local-storage";

@Component({
  selector: "enroll-registration",
  templateUrl: "profile-component.html"
})
export class ProfileComponent implements OnInit {
  public model: TrusteeModel;

  public pendingDataLoadValue: boolean = true;
  public pendingDataSave: boolean = false;
  public lastErrorMessage: string;

  constructor(
    private trusteeService: TrusteeService,
    private profileService: ProfileService,
    private authService: AuthService,
    @Inject(LocalStorage) private localStorage,
    private router: Router
  ) {
    this.model = new TrusteeModel();
  }

  ngOnInit(): void {
    this.pendingDataLoadValue = true;

    const userId = this.authService.userId;
    this.profileService.read(userId)
      .then(result => {
        this.loadTrusteeInfo(result != null ? result.trusteeId : null);
      });
  }

  private loadTrusteeInfo = (guid: string) => {
    if (guid == null) {
      this.model = new TrusteeModel();
      this.pendingDataLoadValue = false;
    } else {
      const params = new URLSearchParams();
      params.append("include", "address");

      this.trusteeService.read(guid, params)
        .then(result => {
          if (result == null)
            console.log("empty trustee result");
          this.model = result || new TrusteeModel();
          this.localStorage.setItem("trusteeId", this.model.id)
        })
        .then(() => {
          this.pendingDataLoadValue = false;
        });
    }
  };

  updateProfile() {
    let promise: Promise<string>;

    this.lastErrorMessage = null;
    this.pendingDataSave = true;

    if (this.model.id == null) {
      promise = this.createTrusteeProfile()
    } else {
      promise = this.updateTrusteeProfile();
    }
    return promise
      .then(this.loadTrusteeInfo)
      .catch(reason => {
        this.lastErrorMessage = reason;
      })
      .then(() => {
        this.pendingDataSave = false;
      });
  }

  private updateTrusteeProfile() {
    const guid = this.model.id;

    return this.trusteeService.update(guid, this.model)
      .then(() => {
        this.loadTrusteeInfo(guid);
      });
  }

  private createTrusteeProfile() {
    return this.trusteeService.create(this.model)
      .then(guid => {
        this.loadTrusteeInfo(guid);
      });
  }

  get showError(): boolean {
    return this.lastErrorMessage != null;
  }

  get errorMessage(): string {
    return this.lastErrorMessage;
  }

  get disableSaveButton(): boolean {
    return this.pendingDataLoadValue || this.pendingDataSave;
  }

  get showSpinnerOnButton(): boolean {
    return this.pendingDataSave;
  }

  get pendingData(): boolean {
    return this.pendingDataLoadValue;
  }

  // fillModel() {
  //   this.model.firstName = "Василий";
  //   this.model.lastName = "Пупкин";
  //   this.model.middleName = "Иванович";
  //   this.model.job = 'ООО "Рога и Копыта"';
  //   this.model.jobPosition = "Директор";
  //   this.model.phoneNumber = "+71235678901";
  //   this.model.email = "pupkinv@riko.ru";
  //   this.model.address = "Москва";
  //   this.model.sex = 0;
  // }
}
