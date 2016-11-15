import {Component, OnInit} from "@angular/core";
import {TrusteeEditModel} from "./trustee.edit.model";
import {Trustee} from "../../../../shared/model/trustee.class";

@Component({
    selector: "enroll-trustee-edit",
    templateUrl: "/views/components/trustee/edit.html",
    providers: [
        TrusteeEditModel
    ]
})
export class TrusteeEditComponent implements OnInit {
    constructor(
        public model: TrusteeEditModel
    ) {        
    }

    ngOnInit() {
        this.model.initialize();
    }

    get trustee(): Trustee {
        return this.model.trustee;
    }
}