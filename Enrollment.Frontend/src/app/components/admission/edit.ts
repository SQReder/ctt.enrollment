import {Component, OnInit} from "@angular/core";
import {URLSearchParams} from "@angular/http";
import {AdmissionService} from "./service";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Admission} from "./admission-class";

@Component({
  selector: "ctt-admission-edit",
  template: "<router-outlet></router-outlet>"
})
export class AdmissionEditComponent implements OnInit{
  model: Admission;

  constructor(
    private admissionService: AdmissionService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
  ) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      let id = params["id"];

      let promise: Promise<Admission>;
      if (id !== undefined) {
        const params = new URLSearchParams();
        params.append("include", "unity");
        params.append("include", "enrollee");
        promise = this.admissionService.read(id, params);
      } else {
        const enrollee = new Admission();
        promise = Promise.resolve(enrollee)
      }

      promise
        .then(enrollee => {
          this.model = enrollee;
          //this.pendingDataValue = false
        });
    });

  }
}
