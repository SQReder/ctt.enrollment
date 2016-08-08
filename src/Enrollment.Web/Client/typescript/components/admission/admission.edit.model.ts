import {Injectable, EventEmitter} from "@angular/core";
import {UnityService} from "../../shared/api/unity.service";
import {TrusteeService} from "../../shared/api/trustee.service";
import {UnityGroup, Unity} from "../../shared/model/unity.class";
import {Enrollee} from "../../shared/model/enrollee.class";
import {Admission} from "../../shared/model/admission.class";
import {GuidGeneratorService} from "../../shared/guidGenerator/guidGenerator.service";
import {RandomIdGenerator} from "../../shared/randomIdGenerator/randomIdGenerator.service"
import * as HttpResults from "../../shared/responses/httpResults";

@Injectable()
export class AdmissionModel {
    enrollees: Enrollee[];
    unityGroups: UnityGroup[];
    unities: Unity[];

    unityGroupIdValue: string;
    unityIdValue: string;
    enrolleeIdValue: string;

    admissionCreated = new EventEmitter<string>();

    constructor(
        private unityService: UnityService,
        private trusteeService: TrusteeService,
        private guidGenerator: GuidGeneratorService,
        private randomIdGenerator: RandomIdGenerator
    ) {
    }

    init() {
        this.unityGroupIdValue = undefined;
        this.unityIdValue = undefined;

        this.unityService
            .getUnityGroups()
            .subscribe((groups: UnityGroup[]) => {
                this.unityGroups = groups;
            });

        this.trusteeService
            .getCurrentTrusteeEnrollees()
            .subscribe((enrollees: Enrollee[]) => {
                this.enrollees = enrollees;
            });
    }

    get isInitialized(): boolean {
        const enrolleeLoaded = this.enrollees != null;
        const unityGroupsLoades = this.unityGroups != null;

        const result = enrolleeLoaded && unityGroupsLoades;
        return result;
    }

    /* UnityGroup */

    get unityGroupId(): string {
        return this.unityGroupIdValue;
    }

    set unityGroupId(unityGroupId: string) {
        this.unityGroupIdValue = unityGroupId;

        this.unityService
            .getUnities(unityGroupId)
            .subscribe((unities: Unity[]) => {
                this.unities = unities;
            });
    }

    get selectedGroup(): UnityGroup {
        return !this.unityGroups
            ? undefined
            : this.unityGroups
                .find(x => x.id === this.unityGroupIdValue, this);
    }

    /* Unity */

    get unityId(): string {
        return this.unityIdValue;
    }

    set unityId(unityId: string) {
        this.unityIdValue = unityId;
    }

    get selectedUnity(): Unity {
        return !this.unities
            ? undefined
            : this.unities
                .find(x => x.id === this.unityIdValue, this);
    }

    /* Enrollee */

    set enrolleeId(enrolleeId: string) {
        this.enrolleeIdValue = enrolleeId;
    }

    get enrolleeId(): string {
        return this.enrolleeIdValue;
    }

    get selectedEnrollee(): Enrollee {
        return !this.enrollees
            ? undefined
            : this.enrollees
                .find(x => x.id === this.enrolleeId, this);
    }

     /* Validation */

    get valid(): boolean {
        const enrolleeValid = this.selectedEnrollee != null;
        const unityValid = this.selectedUnity != null;

        return enrolleeValid && unityValid;
    }

    get generateAdmission(): Admission {
        const admission = new Admission();

        admission.id = this.guidGenerator.create();
        admission.alternateId = this.randomIdGenerator.create().toString();
        admission.enrolleeId = this.enrolleeId;
        admission.unityId = this.unityId;

        return admission;
    }

    createAdmission(): void {
        const admission = this.generateAdmission;

        this.trusteeService.create(admission)
            .subscribe((result: HttpResults.IGenericResult) => {
                if (result.succeeded) {
                    this.admissionCreated.emit(null);
                } else {
                    console.error(result.message);
                }
            });
    }
}