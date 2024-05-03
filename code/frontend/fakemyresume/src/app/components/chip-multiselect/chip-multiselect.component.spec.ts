import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChipMultiselectComponent } from './chip-multiselect.component';

describe('ChipMultiselectComponent', () => {
  let component: ChipMultiselectComponent;
  let fixture: ComponentFixture<ChipMultiselectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChipMultiselectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChipMultiselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
