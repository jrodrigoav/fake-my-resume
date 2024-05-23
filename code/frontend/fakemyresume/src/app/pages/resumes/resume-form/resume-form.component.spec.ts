// import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ResumeFormComponent } from './resume-form.component';
// import { MakeMyResumeService } from '../../../services/make-my-resume/make-my-resume.service';
// import { Observable } from 'rxjs';
// //import { Observable } from 'rxjs/Observable';
// //import 'rxjs/add/observable/of';
// import { of } from 'rxjs';
// import { IEnvironmentConfig, ENV_CONFIG } from '../../../interfaces/environment-config';
// import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
// import { DateAdapter } from '@angular/material/core';
// import { provideNativeDateAdapter } from '@angular/material/core';


// let component: ResumeFormComponent;
// describe('ResumeFormComponent', () => {
  
//  // let fixture: ComponentFixture<ResumeFormComponent>;

//   beforeEach(async () => {
//     // TestBed.configureTestingModule({
//     //   imports: [ResumeFormComponent]
//     // })
//     // .compileComponents();
    
//     // fixture = TestBed.createComponent(ResumeFormComponent);
//     // component = fixture.componentInstance;
//     // fixture.detectChanges();
//     //constructor(private resumeService: MakeMyResumeService, private fb: FormBuilder, private router: Router, route: ActivatedRoute) {

    
// let esz={
//   production: false,
//   apiUrl: 'https://localhost:4200/',
//   msalAuth:{
//       clientId: 'someClient',
//       authority: 'auth',
//       redirectUri: '/',
//       postLogoutRedirectUri: '/'
//   }
// };

// //
// // route.snapshot.params['resumeId'];

// let rte={
//   snapshot:{
//     params: { resumeId: 'new'},
     
//   }
// };


//     await TestBed.configureTestingModule({
//       imports: [ResumeFormComponent,HttpClientTestingModule],
//       providers:[MakeMyResumeService,FormBuilder,Router,{ provide: ActivatedRoute, useValue: rte },{provide:ENV_CONFIG, useValue:esz } ,DateAdapter, provideNativeDateAdapter()]
//     })
    
//     .compileComponents();
//     let fixture: ComponentFixture<ResumeFormComponent>;
//     fixture = TestBed.createComponent(ResumeFormComponent);
//     component = fixture.componentInstance;
//     fixture.detectChanges();
    
//   });

//   it('should createDDD', () => {
//     expect(component).toBeFalse();
//   });
// });

// //  it('should create', () => {
// //     expect(component).toBeTruthy();
// //   });