
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
      fullName: new FormControl('', [Validators.required]),
      currentRole: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      description: new FormControl('', [Validators.required]),
      technologies: new FormControl(''),
      methodologies: new FormControl(),
      certifications: new FormControl()
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
      degree: new FormControl('', [Validators.required]),
      major: new FormControl('', [Validators.required]),
      universityName: new FormControl('', [Validators.required]),
      yearOfCompletion: new FormControl('', [Validators.required])
    });
  }

  submit(): void {
    this.resumeService.saveResume(this.resume).subscribe(response => {
    });
  }
}
