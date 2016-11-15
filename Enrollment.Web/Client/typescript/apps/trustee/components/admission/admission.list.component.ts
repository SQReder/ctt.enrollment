import {Component, OnInit} from "@angular/core";
import {RouteConfig, ROUTER_DIRECTIVES, Router} from "@angular/router-deprecated";
import {AdmissionListModel} from "./admission.list.model";

@Component({
    selector: "enroll-admission-list",
    templateUrl: "/views/components/admission/list.html",
    providers: [
        AdmissionListModel
    ]
})
export class AdmissionListComponent implements OnInit {
    constructor(
        private router: Router,
        public model: AdmissionListModel
    ) {
        this.model.admissionRemoved.subscribe(this.onAdmissionRemoved);
    }

    ngOnInit() {
        this.model.load();
    }

    get haveAnyAdmissions() {
        return this.model.admissions != null &&
            this.model.admissions.length > 0;
    }

    createNew() {
        this.router.navigate(["/Admission/Edit"]);
    }


    onRemoveRequested(id: string) {
        this.model.remove(id);
    }

    onAdmissionRemoved = () => {
        this.model.load();
    }

    onDownloadRequested(id: string) {
        this.model.download(id);
    }
}