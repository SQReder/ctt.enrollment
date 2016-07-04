import { Pipe, PipeTransform } from '@angular/core';

export enum RelationTypeEnum {
    Child = 0, // Сын/Дочь
    Grandchild = 1, // Внук/Внучка
    Ward = 2, // Подопечный
}

/*
 * Raise the value exponentially
 * Takes an exponent argument that defaults to 1.
 * Usage:
 *   value | exponentialStrength:exponent
 * Example:
 *   {{ 2 |  exponentialStrength:10}}
 *   formats to: 1024
*/
@Pipe({ name: 'relationType' })
export class RelationTypeStringPipe implements PipeTransform {
    transform(relation: RelationTypeEnum): string {
        switch (relation) {
            case RelationTypeEnum.Child:
                return 'Сын/Дочь';
            case RelationTypeEnum.Grandchild:
                return 'Внук/Внучка';
            case RelationTypeEnum.Ward:
                return 'Подопечный';
            default:
                throw new RangeError(`Unsupported relation value ${relation}`);
        }
    }
}