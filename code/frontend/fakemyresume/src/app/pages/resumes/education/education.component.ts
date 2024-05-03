import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { Education } from '../../../DTOs/EducationDTO';

@Component({
  selector: 'app-education',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatTableModule, MatIconModule],
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent {
  @Input() resume!: ResumeDTO;
  @Input() educationForm!: FormGroup;

  displayedColumnsEducation: string[] = ['degree', 'major', 'universityName', 'yearOfCompletion', 'country', 'state', 'actions'];

  removeEducation(element: any) {
    this.resume.education.splice(element, 1);
  }

  addEducation() {
    if (this.educationForm.valid) {
      const education: Education = this.educationForm.value;
      this.resume.education = [...this.resume.education, education];
      this.educationForm.reset();
    }
  }
}
