
import { Component, OnDestroy } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { EducationComponent } from './education/education.component';
import { PersonalInformationComponent } from './personal-information/personal-information.component';
import { WorkExperienceComponent } from './work-experience/work-experience.component';
import { MakeMyResumeService } from '../../services/make-my-resume/make-my-resume.service';
import { ResumeDTO } from '../../DTOs/ResumeDTO';
import { Subject, takeUntil } from 'rxjs';


@Component({
  selector: 'app-resumes',
  standalone: true,
  imports: [MatStepperModule, MatButtonModule, WorkExperienceComponent, PersonalInformationComponent, EducationComponent],
  templateUrl: './resumes.component.html',
  styleUrls: ['./resumes.component.css']
})

export class ResumesComponent implements OnDestroy {
  resume: ResumeDTO = new ResumeDTO;
  resumeForm: FormGroup; 
  workExperienceForm: FormGroup;
  educationForm: FormGroup;
  onDestroy$: Subject<void> = new Subject();
  loaded: boolean = false;

  constructor(private resumeService: MakeMyResumeService, private fb: FormBuilder) {
    this.resumeForm = this.fb.group({
      fullName: new FormControl('', [Validators.required]),
      currentRole: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      description: new FormControl('', [Validators.required]),
      certifications: this.fb.array([]),
    });

    this.workExperienceForm = this.fb.group({
      companyName: new FormControl('', [Validators.required]),
      projectName: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      dateBegin: new FormControl(new Date(), [Validators.required]),
      dateEnd: new FormControl(new Date(), [Validators.required]),
      technologies: this.fb.array([]),
    });

    this.educationForm = this.fb.group({
      degree: new FormControl('', [Validators.required]),
      major: new FormControl('', [Validators.required]),
      universityName: new FormControl('', [Validators.required]),
      yearOfCompletion: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required]),
    });

    this.resumeService.getResumes().pipe(takeUntil(this.onDestroy$)).subscribe(resumes => {
      if(resumes.length) {
        const resume = resumes[0];
        this.setResume(resume);
      }
      this.loaded = true;
    });
  }

  ngOnDestroy() {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  submit(): void {
    this.resume = {
      ...this.resume,
      ...this.resumeForm.getRawValue(),
    }
    this.resumeService.saveResume(this.resume).subscribe(resume => {
      this.setResume(resume);
    });
  }

  update(): void {
    this.resume = {
      ...this.resume,
      ...this.resumeForm.getRawValue(),
    }
    if(!this.resume.id) return;
    this.resumeService.updateResume(this.resume.id, this.resume).subscribe(response => {
      // TODO: Handle update
    });
  }

  download() {
    if(!this.resume.id) return;
    this.resumeService.getResumePdF(this.resume.id).subscribe(response => {
      let downloadLink = document.createElement('a');
      downloadLink.href = window.URL.createObjectURL(response);
      downloadLink.setAttribute('download', `${this.resume.fullName} Resume.pdf`);
      document.body.appendChild(downloadLink);
      downloadLink.click();
      document.body.removeChild(downloadLink);
    });
  }

  private setResume(resume: ResumeDTO) {
    (this.resumeForm.controls['certifications'] as FormArray).controls = resume.certifications.map(v => new FormControl());
    this.resumeForm.patchValue(resume);
    this.resume = resume;
  }
}
