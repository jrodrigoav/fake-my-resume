import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormArray, FormControl, ReactiveFormsModule } from '@angular/forms';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { MatChipEditedEvent, MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgForOf } from '@angular/common';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-chip-multiselect',
  standalone: true,
  imports: [NgForOf, ReactiveFormsModule, MatChipsModule, MatIconModule, MatAutocompleteModule, MatFormFieldModule],
  templateUrl: './chip-multiselect.component.html',
  styleUrl: './chip-multiselect.component.css'
})
export class ChipMultiselectComponent {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input({ required: true }) formArray!: FormArray<any>;
  @Input() options: string[] | null = null;
  @Output() onInputChange: EventEmitter<string> = new EventEmitter();
  @Output() onItemSelected: EventEmitter<string> = new EventEmitter();
  inputControl = new FormControl();
  @Input() separatorKeyCodes: number[] = [ENTER, COMMA];

  @ViewChild("itemInput") input!: ElementRef<MatInput>;

  constructor() {
    this.inputControl.valueChanges.subscribe(value => {
      this.onInputChange.emit(value);
    });
  }

  addItem(event: MatChipInputEvent): void {
    const inputValue = event.value.trim();
    if(!inputValue) return;
    // Allow multiple adds if a list of elements separated by comma are added to the input.
    const possibleValues = inputValue.split(',').map(v => v.trim());
    possibleValues.forEach(newValue => {
      // Prevent duplicated values
      if(!this.formArray.value.includes(newValue)) {
        this.formArray.push(new FormControl(newValue));
      }
    });
    event.chipInput.clear();
    this.inputControl.reset();
  }
  
  onItemSelect(event: MatAutocompleteSelectedEvent): void {
    const newValue = event.option.value.trim();
    if(!this.formArray.value.includes(newValue)) {
      this.formArray.push(new FormControl(newValue));
    }
    this.input.nativeElement.value = '';
    this.inputControl.reset();
  }

  editItem(index: number, event: MatChipEditedEvent) {
    if (event.value) {
      this.formArray.controls[index].patchValue(event.value.trim());
    } else {
      this.formArray.removeAt(index);
    }
  }

  removeItem(indexToRemove: number): void {
    this.formArray.controls[indexToRemove].reset();
    this.formArray.removeAt(indexToRemove);
  }
}