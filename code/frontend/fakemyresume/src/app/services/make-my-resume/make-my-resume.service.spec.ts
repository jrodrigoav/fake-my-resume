import { TestBed } from '@angular/core/testing';

import { MakeMyResumeService } from './make-my-resume.service';

describe('MakeMyResumeService', () => {
  let service: MakeMyResumeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MakeMyResumeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
