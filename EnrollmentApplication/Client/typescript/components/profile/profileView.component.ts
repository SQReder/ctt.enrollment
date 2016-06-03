import {Component, Input, Output, OnInit} from 'angular2/core'
import {ProfileService} from '../../shared/profile/profile.service';
import {Profile} from './profile.class';
import {ProfileInfoComponent} from './profileInfo.component';
import {ChildListComponent} from './childList.component';
import * as HttpResults from '../../shared/responses/httpResults';

@Component({
    selector: 'enroll-profile-view',
    templateUrl: '/Profile/ViewLayout',
    directives: [
        ChildListComponent,
        ProfileInfoComponent
    ]
})
export class ProfileViewComponent implements OnInit {
    model: Profile;

    constructor(
        private profileService: ProfileService
    ) {
        this.model = new Profile();
    }

    ngOnInit() {
        this.profileService
            .getCurrentUser()
            .subscribe((result: HttpResults.ProfileInfoResult) => this.model.onProfileInfoLoaded(result));
        this.profileService
            .listChildren()
            .subscribe((result: HttpResults.ListChildrenResult) => this.model.onListChildrenLoaded(result));
    }
}