import { Injectable } from '@angular/core';
import { UniversityTreeElement } from './universityTreeElement';

@Injectable({
  providedIn: 'root'
})
export class ItemsProviderService {
  list: UniversityTreeElement[] = []; 
 
  add(item: UniversityTreeElement) {
      this.list.push(item);
  }

  update(item: UniversityTreeElement) {
    const index = this.findIndexByAttrValue(this.list, 'id', item.id);
    this.list[index] = item;
  }
  
  private findIndexByAttrValue(array, attr, value): number {
    for(var i = 0; i < array.length; i += 1) {
        if(array[i][attr] === value) {
            return i;
        }
    }
    return -1;
  }
}
