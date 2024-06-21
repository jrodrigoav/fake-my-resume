import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Inject, Injectable } from '@angular/core';
import { IEnvironmentConfig, ENV_CONFIG } from '../../interfaces/environment-config';
import { TestBed } from '@angular/core/testing';

import { TechTagsService } from './tech-tags.service';

describe('TechTagsService', () => {
  let service: TechTagsService;

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
        imports: [HttpClientTestingModule ],
        providers: [TechTagsService,{provide:ENV_CONFIG, useValue:env },Injectable,Inject]
    });
    service = TestBed.inject(TechTagsService);
  });

  it('Should be created', () => {
    expect(service).toBeTruthy();
  });

  it('dummy test just to check that all the constructor scaffolding worked', () => {
    expect(2).toBe(2);
  });

  it('test base url get', () => {
    service.searchTechTags("J").subscribe(result => expect(result.length).toBeGreaterThan(0)); 
  });
});
