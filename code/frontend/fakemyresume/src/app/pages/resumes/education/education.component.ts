import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { UtilsService } from '../../../services/utils/utils.service';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { Education } from '../../../DTOs/EducationDTO';

@Component({
  selector: 'app-education',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatTableModule],
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent {
  @Input() resume!: ResumeDTO;
  @Input() educationForm!: FormGroup;

  displayedColumnsEducation: string[] = ['degree', 'major', 'universityName', 'yearOfCompletion', 'remove'];

  constructor(private utilsService: UtilsService) {
    
  }


  removeEducation(element: any) {
    this.resume.education.splice(element, 1);
  }

  addEducation() {
    if (this.educationForm.valid) {
      let education: Education = this.utilsService.mapFormGroupToModel(this.educationForm);
      this.resume.education.push(education);
      this.clearFormGroupValues();
    }
  }

  clearFormGroupValues() {
    Object.keys(this.educationForm.controls).forEach(controlName => {
      const control = this.educationForm.get(controlName);
      if (control instanceof FormControl) {
        control.setValue(null);
      }
    });
  }
  
}
