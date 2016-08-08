import {Component, Input, Output, OnInit} from "@angular/core"
import {DeprecatedTrusteeService as TrusteeService} from "../../shared/trustee/trustee.service";
import {Profile} from "./profile.class";
import {ProfileInfoComponent} from "./profileInfo.component";
import * as HttpResults from "../../shared/responses/httpResults";

@Component({
    selector: "enroll-profile-view",
    templateUrl: "/Profile/ViewLayout",
    directives: [
        ProfileInfoComponent
    ]
})
export class ProfileViewComponent implements OnInit {
    model: Profile;

    constructor(
        private trusteeService: TrusteeService
    ) {
        this.model = new Profile();
    }

    ngOnInit() {
        this.trusteeService
            .getCurrentTrustee()
            .subscribe((result: HttpResults.ITrusteeInfoResult) => this.model.onProfileInfoLoaded(result));
    }
}