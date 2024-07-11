import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { INote } from '../../interfaces/inote';
import { MakeMyResumeService } from '../../services/make-my-resume/make-my-resume.service';
@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [NgFor, MatGridListModule, MatCardModule, MatInputModule, MatFormFieldModule, FormsModule, MatIconModule, MatButtonModule],
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.css'
})
export class NotesComponent implements OnInit {
  notes: INote[];

  constructor(private makeMyResume: MakeMyResumeService) {
    this.notes = [];
  }

  ngOnInit() {
    this.refreshNotes();
  }

  refreshNotes() {
    this.makeMyResume.fetchNotes().subscribe(n=>this.notes = n);
  }

  addNote() {
    this.notes.push({
      createdAt: new Date(),
      title: '',
      content: '',
      isDraft: true
    });
  }

  saveNote(event: Event, index: number) {
    event.preventDefault();
    const newNote = this.notes[index];    
    if (newNote.isDraft) {
      this.makeMyResume.createNote(newNote).subscribe(n => {        
        this.notes[index].createdAt = n.createdAt;
        this.notes[index].isDraft = false;
      });
    }
    else {      
      this.makeMyResume.updateNote(newNote).subscribe();      
    }
  }

  deleteNote(event: Event, index: number) {
    event.preventDefault();
    const deletedNote = this.notes[index];    
    if (!deletedNote.isDraft) {
      this.makeMyResume.deleteNote(deletedNote).subscribe();      
    }
    this.notes.splice(index, 1);
  }
}
