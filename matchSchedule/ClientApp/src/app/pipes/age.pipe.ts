import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'calculateAge',
})
export class CalculateAgePipe implements PipeTransform {
  transform(birthdate: Date): number {
    const _birthdate = new Date(birthdate);
    const today = new Date();
    const age = today.getFullYear() - _birthdate.getFullYear();
    const monthDiff = today.getMonth() - _birthdate.getMonth();

    if (
      monthDiff < 0 ||
      (monthDiff === 0 && today.getDate() < _birthdate.getDate())
    ) {
      return age - 1;
    }

    return age;
  }
}
