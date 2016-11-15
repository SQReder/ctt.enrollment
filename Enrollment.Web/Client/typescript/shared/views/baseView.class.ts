import * as HttpResults from "../responses/httpResults";
import {Notifier} from "../uikit/notify.service";

export abstract class BaseViewModel {
    inErrorState: boolean;

    constructor(
        protected notifier: Notifier
    ) { }

    protected requestErrorHandler(error: HttpResults.IErrorResult): void {
        this.inErrorState = true;
        console.error(error.message, error);
    }

    abstract initialize(): void;
}
