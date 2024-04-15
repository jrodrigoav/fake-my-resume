
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { EducationComponent } from './education/education.component';
import { PersonalInformationComponent } from './personal-information/personal-information.component';
import { WorkExperienceComponent } from './work-experience/work-experience.component';
import { MakeMyResumeService } from '../../services/make-my-resume/make-my-resume.service';
import { ResumeDTO } from '../../DTOs/ResumeDTO';


@Component({
  selector: 'app-resumes',
  standalone: true,
  imports: [MatStepperModule, MatButtonModule, WorkExperienceComponent, PersonalInformationComponent, EducationComponent],
  templateUrl: './resumes.component.html',
  styleUrls: ['./resumes.component.css']
})

export class ResumesComponent {
  resume: ResumeDTO = new ResumeDTO;
  resumeForm: FormGroup; 
  workExperienceForm: FormGroup;
  educationForm: FormGroup;

  constructor(private resumeService: MakeMyResumeService, private fb: FormBuilder) {
    this.resumeForm = this.fb.group({
      Fullname: new FormControl('', [Validators.required]),
      CurrentRole: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Description: new FormControl('', [Validators.required]),
      Technologies: new FormControl(''),
      Methodologies: new FormControl(),
      Certifications: new FormControl()
    });

    this.workExperienceForm = this.fb.group({
      companyName: new FormControl('', [Validators.required]),
      projectName: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      dateBegin: new FormControl(new Date(), [Validators.required]),
      dateEnd: new FormControl(new Date(), [Validators.required])
    });

    this.educationForm = this.fb.group({
      Degree: new FormControl('', [Validators.required]),
      Major: new FormControl('', [Validators.required]),
      UniversityName: new FormControl('', [Validators.required]),
      YearOfCompletion: new FormControl('', [Validators.required])
    });
  }

  submit(): void {
    this.resumeService.saveResume(this.resume).subscribe(response => {
    });
  }
}
