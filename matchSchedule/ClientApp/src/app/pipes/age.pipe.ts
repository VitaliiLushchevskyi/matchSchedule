import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'calculateAge'
})
export class CalculateAgePipe implements PipeTransform {
  transform(birthdate: string): number {
    const birthDate = new Date(birthdate);
    const today = new Date();
    const age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();
    
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
      return age - 1;
    }
    
    return age;
  }
}