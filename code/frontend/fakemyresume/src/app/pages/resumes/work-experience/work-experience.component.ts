import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AsyncPipe, DatePipe, NgIf } from '@angular/common';
import { debounceTime, switchMap } from 'rxjs/operators';
import { Observable, Subject, of } from 'rxjs';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { TechTagsService } from '../../../services/tech-tags/tech-tags.service';
import { ChipMultiselectComponent } from '../../../components/chip-multiselect/chip-multiselect.component';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { WorkExperience } from '../../../DTOs/WorkExperienceDTO';
import { DatePipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-work-experience',
  standalone: true,
  imports: [NgIf, AsyncPipe, DatePipe, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDatepicker, MatDatepickerModule, MatTableModule, MatIconModule, ChipMultiselectComponent],
  templateUrl: './work-experience.component.html',
  styleUrls: ['./work-experience.component.css']
})
export class WorkExperienceComponent implements OnInit {
  @Input() resume = new  ResumeDTO();
  @Input() workExperienceForm!: FormGroup;
  columnsToDisplayExperience: any[] = ['companyName', 'role', 'description', 'projectName', 'technologies', 'from', 'to', 'actions'];
  filteredTechs: Observable<string[]> = new Observable<string[]>();
  inputChanges: Subject<string> = new Subject();
  technologiesControl!: FormArray;
  
  constructor(private cd: ChangeDetectorRef, private techTagsService: TechTagsService) {
    this.filteredTechs = this.inputChanges.pipe(
      debounceTime(300),
      switchMap(value => {
        if(!value?.trim()) return of([]);
        return this.techTagsService.searchTechTags(value);
      })
    );
  }

  ngOnInit() {
    this.technologiesControl = this.workExperienceForm.controls['technologies'] as FormArray;
  }

  addExperience() {
    if (this.workExperienceForm.valid) {
      const experience: WorkExperience = this.workExperienceForm.value;
      this.resume.workExperience = [...this.resume.workExperience, experience];
      this.workExperienceForm.reset();
      this.technologiesControl.clear();
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
    this.resume.workExperience = this.resume.workExperience.slice();
  }
}


