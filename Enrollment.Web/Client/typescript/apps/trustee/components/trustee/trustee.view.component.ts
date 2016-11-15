import {Component, OnInit} from "@angular/core";
import {TrusteeViewModel} from "./trustee.view.model";
import {Router} from "@angular/router-deprecated";

@Component({
    selector: "enroll-trustee-view",
    templateUrl: "/views/components/trustee/view.html",
    providers: [
        TrusteeViewModel
    ]
})
export class TrusteeViewComponent implements OnInit {
    constructor(
        public model: TrusteeViewModel,
        private router: Router
    ) {
    }

    ngOnInit() {
        this.model.initialize();
    }

    get isInitialized(): boolean {
        return this.model.trustee != null;
    }

    onEditRequested() {
        this.router.navigate(["Edit"]);
    }
}