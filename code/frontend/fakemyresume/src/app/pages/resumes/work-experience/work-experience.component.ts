import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { UtilsService } from '../../../services/utils/utils.service';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { WorkExperience } from '../../../DTOs/WorkExperienceDTO';

@Component({
  selector: 'app-work-experience',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDatepicker, MatDatepickerModule, MatTableModule],
  templateUrl: './work-experience.component.html',
  styleUrls: ['./work-experience.component.css']
})
export class WorkExperienceComponent implements OnInit {
  @Input() resume = new  ResumeDTO();
  @Input() workExperienceForm!: FormGroup;
  dataSourceExperience: WorkExperience[] = [];
  columnsToDisplayExperience: any[] = [
    'companyName',
    'role',
    'description',
    'projectName',
    'fromMonth',
    'fromYear',
    'toMonth',
    'toYear'
  ];
 
  constructor(private cd: ChangeDetectorRef, private utilsService : UtilsService){
  }

  ngOnInit(): void {

    this.resume.workExperience = [];
  }

  addExperience(){
    if (this.workExperienceForm.valid) {
      let experience: WorkExperience = this.utilsService.mapFormGroupToModel(this.workExperienceForm);
      this.dataSourceExperience.push(experience);
      this.resume.workExperience = this.dataSourceExperience;
      this.utilsService.clearFormGroupValues(this.workExperienceForm);
      }
  }

  chosenMonthHandler(dateControlName: string, normlizedMonth: Date, datepicker: MatDatepicker<Date>) {
    const miFormControl = this.workExperienceForm.get(dateControlName);
    if (miFormControl) {
      miFormControl.setValue(normlizedMonth);
      this.cd.detectChanges();
      datepicker.close();
    }
  }

}


