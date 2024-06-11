import { Component, OnDestroy } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { EducationComponent } from './../education/education.component';
import { PersonalInformationComponent } from './../personal-information/personal-information.component';
import { WorkExperienceComponent } from './../work-experience/work-experience.component';
import { MakeMyResumeService } from '../../../services/make-my-resume/make-my-resume.service';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgIf } from '@angular/common';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-resume-form',
  standalone: true,
  imports: [NgIf, MatStepperModule, MatButtonModule, WorkExperienceComponent, PersonalInformationComponent, EducationComponent, MatIcon],
  templateUrl: './resume-form.component.html',
  styleUrl: './resume-form.component.css'
})
export class ResumeFormComponent implements OnDestroy {
  resume!: ResumeDTO;
  resumeForm: FormGroup;
  workExperienceForm: FormGroup;
  educationForm: FormGroup;
  onDestroy$: Subject<void> = new Subject();
  

  constructor(private resumeService: MakeMyResumeService, private fb: FormBuilder, private router: Router, route: ActivatedRoute) {
    this.resumeForm = this.fb.group({
      fullName: new FormControl('', [Validators.required]),
      currentRole: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      certifications: new FormArray([]),
    });

    this.workExperienceForm = this.fb.group({
      companyName: new FormControl('', [Validators.required]),
      projectName: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      dateBegin: new FormControl(new Date(), [Validators.required]),
      dateEnd: new FormControl(new Date(), [Validators.required]),
      technologies:new FormArray([]), 
    });

    this.educationForm = this.fb.group({
      degree: new FormControl('', [Validators.required]),
      major: new FormControl('', [Validators.required]),
      universityName: new FormControl('', [Validators.required]),
      yearOfCompletion: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required]),
    });

    const resumeId = route.snapshot.params['resumeId'];
    if(resumeId == "new") {
      this.resume = new ResumeDTO();
    } else if(resumeId) {
      this.resumeService.getResume(resumeId).subscribe(resume => {
        this.resume = resume;
        this.resume.certifications=resume.certifications;
        this.setResume(this.resume);
      });
    }
  }

  back() {
    this.router.navigate(["resumes"]);
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
    this.router.navigate(["resumes"]);
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
