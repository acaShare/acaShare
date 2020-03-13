import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({name: 'polishDate'})
export class PolishDatePipe implements PipeTransform {
  transform(date: Date, format: string = 'd.MM.yyyy, HH:mm'): string {
    return new DatePipe("en-US").transform(date, format);
  }
}