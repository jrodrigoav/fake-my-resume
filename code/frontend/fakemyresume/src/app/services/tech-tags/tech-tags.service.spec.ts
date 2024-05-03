import { TestBed } from '@angular/core/testing';

import { TechTagsService } from './tech-tags.service';

describe('TechTagsService', () => {
  let service: TechTagsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TechTagsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
