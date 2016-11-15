import {Component, OnInit} from "@angular/core"
import {URLSearchParams} from "@angular/http"
import {Router} from "@angular/router"
import {EnrolleeService, Enrollee} from "./enrollee-service";
import {TrusteeService} from "../../services/api/trustee-service";
import {ProfileService} from "../../services/api/profile-service";
import {AuthService} from "../../services/auth/auth-service";

@Component({
  selector: "ctt-enrollee-list",
  templateUrl: "enrollee-list-component.html"
})
export class EnrolleeListComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private profileService: ProfileService,
    private enrolleeService: EnrolleeService,
    private trusteeService: TrusteeService,
    private router: Router
  ) {
  }

  private enrollees: Enrollee[];
  private pendingDataValue = true;

  ngOnInit(): void {
    this.pendingDataValue = true;
    this.loadEnrolleees();
  }

  private loadEnrolleees() {
    const userId = this.authService.userId;
    this.profileService.read(userId)
      .then(profile => {
        const params = new URLSearchParams();
        params.append("trustee", profile.trusteeId);

        this.enrolleeService.list(params)
          .then(enrollees => {
            this.enrollees = enrollees;
            this.pendingDataValue = false;
          });
      });
  }

  get isDataLoaded(): boolean {
    return !this.pendingDataValue;
  }

  createNew() {
    this.router.navigateByUrl("/enrollee/new")
  }

  onRequestEdit(guid: string) {
    this.router.navigateByUrl("/enrollee/edit/" + guid)
  }

  onRequestRemove(guid: string) {
    this.enrolleeService.delete(guid)
      .then(() => {
        this.loadEnrolleees();
      })
  }
}
