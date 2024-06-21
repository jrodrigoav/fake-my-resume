import { Inject, Injectable } from '@angular/core';
import { IEnvironmentConfig, ENV_CONFIG } from '../../interfaces/environment-config';
// Http testing module and mocking controller
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

// Other imports
import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { MakeMyResumeService } from './make-my-resume.service';

describe('MakeMyResumeService', () => {
  let service: MakeMyResumeService;

  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
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

  beforeEach(() => {
    
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [MakeMyResumeService, { provide: ENV_CONFIG, useValue: env },Injectable,Inject]
    });

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);

    service = TestBed.inject(MakeMyResumeService);

   
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should be created', () => {
    expect(service.baseUrl).toBe(env.apiUrl);
  });

});
