import {Component, Input, Output, OnInit} from "@angular/core"
import {TrusteeInfo} from "./TrusteeInfo.class";

@Component({
    selector: "enroll-profile-info",
    templateUrl: "/Profile/InfoLayout"
})
export class ProfileInfoComponent {
    @Input() model: TrusteeInfo
}