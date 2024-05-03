import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { TextService } from '../../services/text/text.service';
import { MatDialog } from '@angular/material/dialog';
import { SuggestionsDialogComponent } from './suggestions-dialog/suggestions-dialog.component';

@Component({
  selector: 'app-text-format-button',
  standalone: true,
  imports: [MatButtonModule, SuggestionsDialogComponent],
  templateUrl: './text-format-button.component.html',
  styleUrl: './text-format-button.component.css'
})
export class TextFormatButtonComponent {
  @Input() value?: string;
  @Input() label: string = "Review format and suggestions";
  busy: boolean = false;

  constructor(private readonly textService: TextService, public dialog: MatDialog) {
    
  }

  async formatText(value: string) {
    if(this.busy) return;
    this.busy = true;
    this.textService.checkFormatAndSuggestions(value).subscribe({
      next: (suggestedChange) => {
        if(!suggestedChange) {
          suggestedChange = "Couldn't get any format changes or suggestions.";
        }
        this.openDialog(suggestedChange);
      },
      error: () => {
        this.busy = false;
      },
      complete: () => {
        this.busy = false;
      }
    });
  }

  openDialog(data: string): void {
    this.dialog.open(SuggestionsDialogComponent, {
      data,
    });
  }
}
