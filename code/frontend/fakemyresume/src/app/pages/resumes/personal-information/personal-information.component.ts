import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { AsyncPipe, NgFor } from '@angular/common';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable, debounceTime, map, startWith } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { ResumeDTO } from '../../../DTOs/ResumeDTO';
import { TechTagsService } from '../../../services/tech-tags/tech-tags.service';

@Component({
  selector: 'app-personal-information',
  standalone: true,
  imports: [NgFor, AsyncPipe, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatChipsModule, MatIconModule, MatAutocompleteModule],
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.css']
})
export class PersonalInformationComponent implements OnInit {
  @Input() resume!: ResumeDTO;
  @Input() resumeForm!: FormGroup; 
  techControl = new FormControl();
  methControl = new FormControl();
  certControl = new FormControl();

  filteredTechs!: Observable<string[]>;

  allTechs: string[] = [];
  techs: string[] = [];
  meths: string[] = [];
  certs: string[] = [];

  @ViewChild('techInput')
  techInput!: ElementRef<HTMLInputElement>;
  @ViewChild('certInput') certInput!: ElementRef<HTMLInputElement>;

  separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(private techTagsService: TechTagsService) {
    this.filteredTechs = this.techControl.valueChanges.pipe(
      debounceTime(300),
      startWith(null),
      map((tech: string | null) => (tech ? this._filterTech(tech) : this.allTechs.slice())),
    );
  }

  ngOnInit(): void {

  }

  selectTech(event: MatAutocompleteSelectedEvent): void {
    this.techs.push(event.option.viewValue);
    this.resume.technologies.push(event.option.viewValue);
    this.techInput.nativeElement.value = '';
  }

  private _filterTech(value: string): string[] {
    let techTags: string[] = [];
    let tags: string[] = [];
    this.techTagsService.searchTechTags(value).subscribe({
      next: response => {
        techTags = response;
      },
      complete: () => {
        techTags.forEach(item => {
          tags.push(item);
        });
      }
    });
    return tags;
  }

  addMeth(event: MatChipInputEvent): void {
    this.resume.methodologies = this.resume.methodologies.filter(p => p !== '');
    this.manageItem(event.value, this.meths, this.resume.methodologies, 'add');
    event.chipInput!.clear();
    this.techControl.setValue(null);
  }

  addCert(event: MatChipInputEvent): void {
    this.resume.certifications = this.resume.certifications.filter(p => p !== '');
    this.manageItem(event.value, this.certs, this.resume.certifications, 'add');
    event.chipInput!.clear();
    this.techControl.setValue(null);
  }

  addTech(event: MatChipInputEvent): void {
    this.resume.technologies = this.resume.technologies.filter(p => p !== '');
    this.manageItem(event.value, this.techs, this.resume.technologies, 'add');
    event.chipInput!.clear();
    this.techControl.setValue(null);
  }

  editMeth(methodology: string, event: MatChipEditedEvent) {
    this.manageItem(event.value, this.techs, this.resume.methodologies, 'edit', methodology);
  }

  editCert(certification: string, event: MatChipEditedEvent) {
    this.manageItem(event.value, this.certs, this.resume.technologies, 'edit', certification);
  }

  editTech(oldTech: string, event: MatChipEditedEvent) {
    this.manageItem(event.value, this.techs, this.resume.technologies, 'edit', oldTech);
  }
  removeMeth(meth: string): void {
    this.manageItem(meth, this.meths, this.resume.methodologies, 'remove');
  }

  removeCert(cert: string): void {
    this.manageItem(cert, this.certs, this.resume.technologies, 'remove');
  }

  removeTech(tech: string): void {
    this.manageItem(tech, this.techs, this.resume.technologies, 'remove');
  }

  private manageItem(
    value: string,
    array: string[],
    resumeArray: string[],
    action: 'add' | 'edit' | 'remove',
    oldValue?: string
  ): void {

    const trimmedValue = value.trim();

    switch (action) {
      case 'add':
        if (trimmedValue) {
          array.push(trimmedValue);
          resumeArray.push(trimmedValue);
        }
        break;

      case 'edit':
        const index = array.indexOf(oldValue || '');
        if (index >= 0 && trimmedValue) {
          array[index] = trimmedValue;
          resumeArray[index] = trimmedValue;
        } else if (index >= 0) {
          array.splice(index, 1);
          resumeArray.splice(index, 1);
        }
        break;

      case 'remove':
        const indexToRemove = array.indexOf(trimmedValue);
        if (indexToRemove >= 0) {
          array.splice(indexToRemove, 1);
          resumeArray.splice(indexToRemove, 1);
        }
        break;
    }
  }
}

