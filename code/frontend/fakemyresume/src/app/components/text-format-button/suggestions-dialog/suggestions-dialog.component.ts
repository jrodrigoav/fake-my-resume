import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose } from '@angular/material/dialog';
import { ClipboardModule } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-suggestions-dialog',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    ClipboardModule,
  ],
  templateUrl: './suggestions-dialog.component.html',
  styleUrl: './suggestions-dialog.component.css'
})
export class SuggestionsDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<SuggestionsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
  ) {}
}
