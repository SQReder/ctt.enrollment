import {Component, Input, Output, OnInit} from '@angular/core'
import {ProfileInfo} from './profileInfo.class';

@Component({
    selector: 'enroll-profile-info',
    templateUrl: '/Profile/InfoLayout'
})
export class ProfileInfoComponent {
    @Input() model: ProfileInfo
}