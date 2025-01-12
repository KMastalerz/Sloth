import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarType } from '../snackbar.model';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  readonly snackBar = inject(MatSnackBar);
  
  openSnackbar(message: string, buttonLabel: string = 'Close', duration: number = 5000, type: SnackbarType = SnackbarType.INFORMATION) {
      this.snackBar.open(message, buttonLabel, {
        duration: duration,
        panelClass: type
      });
  }
}
