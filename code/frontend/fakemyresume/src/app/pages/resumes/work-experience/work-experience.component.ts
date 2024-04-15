import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { UtilsService } from '../../../services/utils/utils.service';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { WorkExperience } from '../../../DTOs/WorkExperienceDTO';
import { DatePipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-work-experience',
  standalone: true,
  imports: [NgIf, DatePipe, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDatepicker, MatDatepickerModule, MatTableModule, MatIconModule],
  templateUrl: './work-experience.component.html',
  styleUrls: ['./work-experience.component.css']
})
export class WorkExperienceComponent {
  @Input() resume = new  ResumeDTO();
  @Input() workExperienceForm!: FormGroup;
  columnsToDisplayExperience: any[] = ['companyName', 'role', 'description', 'projectName', 'from', 'to', 'actions'];
 
  constructor(private cd: ChangeDetectorRef, private utilsService : UtilsService) {

  }

  addExperience(){
    if (this.workExperienceForm.valid) {
      let experience: WorkExperience = this.utilsService.mapFormGroupToModel(this.workExperienceForm);
      this.resume.workExperience = [...this.resume.workExperience, experience];
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

  removeRow(index: number) {
    this.resume.workExperience.splice(index, 1);
  }
}


