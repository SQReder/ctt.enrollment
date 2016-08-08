export class RandomIdGenerator {
    constructor(
        private maxValue
    ) { }

    create(): number {
        return Math.floor(Math.random() * this.maxValue);
    }
}