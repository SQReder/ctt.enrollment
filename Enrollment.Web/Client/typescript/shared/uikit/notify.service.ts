import {Injectable} from "@angular/core";

// ReSharper disable once InconsistentNaming
declare var UIkit;

export abstract class Notifier {
    abstract info(message: string);

    abstract success(message: string);

    abstract error(message: string);

    abstract warning(message: string);
}

export class UIkitNotifier extends Notifier {
    constructor(
        private position: string = "bottom-right"
    ) {
        super();
    }

    info(message: string) {
        this.notify(message, "info");
    }

    success(message: string) {
        this.notify(message, "success");
    }

    error(message: string) {
        this.notify(message, "error");
    }

    warning(message: string) {
        this.notify(message, "warning");
    }

    private notify(message: string, level: string) {
        UIkit.notify(message,
        {
            status: level,
            pos: this.position
        });
    }
}