
import { Component, OnInit } from '@angular/core';
import { ResumeDTO } from 'src/app/DTOs/ResumeDTO';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TechTagsService } from 'src/app/services/tech-tags/tech-tags.service';
import { MakeMyResumeService } from 'src/app/services/make-my-resume/make-my-resume.service';

@Component({
  selector: 'app-resumes',
  templateUrl: './resumes.component.html',
  styleUrls: ['./resumes.component.css']
})

export class ResumesComponent implements OnInit {
  resume: ResumeDTO = new ResumeDTO;
  resumeForm!: FormGroup; 
  workExperienceForm!: FormGroup;
  educationForm!: FormGroup;

  constructor( public techTagsService: TechTagsService, private resumeService: MakeMyResumeService, private fb: FormBuilder) {

  }


  ngOnInit(): void {

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


  submit(resumeForm: any): void {

    /* this.resumeService.dummyResume().subscribe(response => {
      let fileName = "my-resume";
      let blob:Blob = response.body as Blob;
      let a = document.createElement('a');
      a.download = fileName;
      a.href = window.URL.createObjectURL(blob);
      a.click();
    }); */

    console.log(this.resume);
    this.resumeService.saveResume(this.resume).subscribe(response => {
      // let fileName = "my-resume";
      // let blob:Blob = response.body as Blob;
      // let a = document.createElement('a');
      // a.download = fileName;
      // a.href = window.URL.createObjectURL(blob);
      // a.click();
      console.log(response)
    });
  }

  download(): void {
    this.resumeService.downloadResume(this.resume.accountId).subscribe(response => {
      let fileName = this.resume.fullname + " " + "CV";
      let blob:Blob = response.body as Blob;
      let a = document.createElement('a');
      a.download = fileName;
      a.href = window.URL.createObjectURL(blob);
      a.click();
    });
  }

}
