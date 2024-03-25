import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor() { }

  mapFormGroupToModel(formGroup: FormGroup): any {
    const model = {} as any;
  
    Object.keys(formGroup.controls).forEach(controlName => {
      const control = formGroup.get(controlName);
      if (control) {
        model[controlName] = control.value;
      }
    });
  
    return model;
  }

  clearFormGroupValues(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(controlName => {
      const control = formGroup.get(controlName);
      if (control instanceof FormControl) {
        control.setValue(null); 
      }
    });
  }
}
