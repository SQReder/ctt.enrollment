import {Injectable} from "@angular/core";
import {Observable} from "rxjs/Observable";

declare var UIkit: any;

export abstract class ModalService {
    abstract confirm(message: string): Observable<boolean>;
}

export class UIkitModalService {
    confirm(message: string): Observable<boolean> {
        return Observable.create(observer => {
            UIkit.modal.confirm(message, () => {
                observer.next(true);
                observer.complete();
            }, () => {
                observer.next(false);
                observer.complete();
            });
        });
    }
}