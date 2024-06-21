
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MakeMyResumeService } from '../../services/make-my-resume/make-my-resume.service';
import { ResumeDTO } from '../../DTOs/ResumeDTO';
import { Observable } from 'rxjs';
import { AsyncPipe, NgIf } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ResumeFormComponent } from './resume-form/resume-form.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-resumes',
  standalone: true,
  imports: [NgIf, AsyncPipe, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, ResumeFormComponent],
  templateUrl: './resumes.component.html',
  styleUrls: ['./resumes.component.css']
})

export class ResumesComponent {
  resumes: Observable<ResumeDTO[]>;
  enableForm: boolean = false;
  tableColumns = [ "id", "fullName", "currentRole", "description", "actions" ];

  constructor(private resumeService: MakeMyResumeService, private router: Router) {
    this.resumes = this.resumeService.getResumes();
  }

  addResume() {
    this.router.navigate(["resumes", "new"]);
  }

  editResume(resume: ResumeDTO) {
    this.router.navigate(["resumes", resume.id]);
  }

  downloadResume(resume: ResumeDTO) {
    if(!resume.id) return;
    this.resumeService.getResumePdF(resume.id).subscribe(response => {
      let downloadLink = document.createElement('a');
      downloadLink.href = window.URL.createObjectURL(response);
      downloadLink.setAttribute('download', `resume_unosquare.pdf`);
      document.body.appendChild(downloadLink);
      downloadLink.click();
      document.body.removeChild(downloadLink);
    });
  }
}
