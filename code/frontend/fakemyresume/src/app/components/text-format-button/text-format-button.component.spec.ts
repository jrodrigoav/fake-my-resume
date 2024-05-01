import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextFormatButtonComponent } from './text-format-button.component';

describe('TextFormatButtonComponent', () => {
  let component: TextFormatButtonComponent;
  let fixture: ComponentFixture<TextFormatButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextFormatButtonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TextFormatButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
