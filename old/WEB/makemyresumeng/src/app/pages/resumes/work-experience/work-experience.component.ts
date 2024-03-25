import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { ResumeDTO } from 'src/app/DTOs/ResumeDTO';
import { WorkExperience } from 'src/app/DTOs/WorkExperienceDTO';
import { UtilsService } from 'src/app/services/utils/utils.service';

@Component({
  selector: 'app-work-experience',
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


