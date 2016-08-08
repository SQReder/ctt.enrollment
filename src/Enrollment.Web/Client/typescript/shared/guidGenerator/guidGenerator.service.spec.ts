"use strict";

import {GuidGeneratorService} from "./guidGenerator.service"

describe("GuidGeneratorService", () => {
    var service: any;

    beforeEach(() => {
        service = new GuidGeneratorService();
    });

    describe("create method", () => {
        it("must return not null", () => {
            const guid = service.create();

            expect(guid).toBeDefined();
            expect(guid).not.toBeNull();
        });

        it("must fit in guid mask", () => {
            const guid = service.create();

            expect(guid).toMatch(/[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}/);
        });

        it("must generate different values in sequental calls", () => {
            const first = service.create();
            const second = service.create();

            expect(first !== second).toBe(true);
        });
    });
});