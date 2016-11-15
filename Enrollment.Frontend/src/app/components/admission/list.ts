import {URLSearchParams} from "@angular/http"
import {Component, OnInit, Inject} from "@angular/core";
import {Router} from "@angular/router"
import {AdmissionService} from "./service";
import {LocalStorage} from "../../services/local-storage";
import {Admission} from "./admission-class";

@Component({
  selector: "ctt-admission-list",
  templateUrl: "list.html"
})
export class AdmissionListComponent implements OnInit {
  constructor(
    private admissionService: AdmissionService,
    private router: Router,
    @Inject(LocalStorage) private localStorage
  ) {}

  private initializedValue = false;
  admissions: Admission[];

  ngOnInit(): void {
    this.loadAdmissionList();
  }

  private loadAdmissionList(): Promise<never> {
    const params = new URLSearchParams();
    params.append("trustee", this.localStorage["trusteeId"]);
    return this.admissionService.list(params)
      .then(admissions => {
        this.admissions = admissions;
        this.initializedValue = true;
      });
  }

  get initialized(): boolean {
    return this.initializedValue;
  }

  createNew() {
    this.router.navigateByUrl("/admission/new")
  }

  onRequestDelete(guid: string) {
    this.admissionService.delete(guid)
      .then(() => {
        this.loadAdmissionList()
      })
  }
}
