import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkExperienceComponent } from './work-experience.component';
import { TechTagsService } from '../../../services/tech-tags/tech-tags.service';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Inject, Injectable } from '@angular/core';
import { IEnvironmentConfig, ENV_CONFIG } from '../../../interfaces/environment-config';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ResumesComponent } from '../resumes.component';

import { FormGroup, FormControl,FormArray, FormBuilder } from '@angular/forms'
 

describe('WorkExperienceComponent', () => {
  let component: WorkExperienceComponent;
  let fixture: ComponentFixture<WorkExperienceComponent>;
  
let env={
  production: false,
  apiUrl: 'https://localhost:4200/',
  msalAuth:{
      clientId: 'someClient',
      authority: 'auth',
      redirectUri: '/',
      postLogoutRedirectUri: '/'
  }
};



  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkExperienceComponent,HttpClientTestingModule ,MatDatepickerModule,MatNativeDateModule  ],
      providers:[WorkExperienceComponent,ChangeDetectorRef,TechTagsService,{provide:ENV_CONFIG, useValue:env },Injectable,Inject]

    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkExperienceComponent);
    component = fixture.componentInstance;
    component.workExperienceForm = new FormGroup({
      technologies: new FormArray([ ]),
      companyName:  new FormControl(""),
      projectName:  new FormControl(""), 
      role:  new FormControl(""),
      description:  new FormControl(""),
      dateBegin:  new FormControl(""),
      dateEnd:  new FormControl(""),
    } );
    fixture.detectChanges();
  });


  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
