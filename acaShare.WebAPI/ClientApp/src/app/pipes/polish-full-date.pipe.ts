import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'polishFullDate'
})
export class PolishFullDatePipe implements PipeTransform {

  transform(date: Date, format: string = 'd MMMM yyyy, HH:mm'): string {
    return new DatePipe("en-US").transform(date, format);
  }

}
