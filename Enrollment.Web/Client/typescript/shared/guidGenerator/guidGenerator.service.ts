"use strict";

import {Injectable} from "@angular/core";

@Injectable()
export class GuidGeneratorService {
    create(): string {
        // UUID by RFC 4122 from http://stackoverflow.com/a/2117523
        // for 4 and y see 4.1.1 and 4.1.3 section of RFC document
        const guid = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, c => {
            const r = Math.random() * 16 | 0;
            const v = c === "x" ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        return guid;
    }
};