import { Component, Input } from '@angular/core';
import { AsyncPipe, NgFor } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormArray } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { ChipMultiselectComponent } from '../../../components/chip-multiselect/chip-multiselect.component';

@Component({
  selector: 'app-personal-information',
  standalone: true,
  imports: [NgFor, AsyncPipe, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatChipsModule, MatIconModule, ChipMultiselectComponent],
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.css']
})
export class PersonalInformationComponent {
  @Input() resume!: ResumeDTO;
  @Input() resumeForm!: FormGroup;
  certificationsControl!: FormArray;

  ngOnInit() {
    this.certificationsControl = this.resumeForm.controls['certifications'] as FormArray;
  }
}

