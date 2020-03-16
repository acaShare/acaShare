import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent {
  @Input() submitButtonText?: string = "Tak";
  @Input() closeButtonText?: string = "Anuluj";
  @Output() submit = new EventEmitter<number>();

  isVisible = false;
  isLoading = false;
  innerText: string;
  id: number;

  onSubmit() {
    this.isLoading = true;
    this.submit.emit(this.id);
    this.isLoading = false;
    this.isVisible = false;
  }

  toggleModal(innerText: string, id: number) {
    this.isVisible = !this.isVisible;
    this.innerText = innerText; 

    if (this.isVisible === true) {
      this.id = id;
    }
  }
}
