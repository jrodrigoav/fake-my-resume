import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Education } from 'src/app/DTOs/EducationDTO';
import { ResumeDTO } from 'src/app/DTOs/ResumeDTO';
import { UtilsService } from 'src/app/services/utils/utils.service';

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {
  @Input() resume!: ResumeDTO;
  @Input() educationForm!: FormGroup;

  displayedColumnsEducation: string[] = ['degree', 'major', 'universityName', 'yearOfCompletion', 'remove'];
  dataSourceEducation: Education[] = [];

  constructor(private utilsService: UtilsService){}
  ngOnInit(): void {
    this.resume.education = [];
  }


  removeEducation(element: any) {
    this.dataSourceEducation.splice(element, 1);
  }

  addEducation() {
    if (this.educationForm.valid) {
      let education: Education = this.utilsService.mapFormGroupToModel(this.educationForm);
      this.dataSourceEducation.push(education);
      this.resume.education = this.dataSourceEducation;
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
