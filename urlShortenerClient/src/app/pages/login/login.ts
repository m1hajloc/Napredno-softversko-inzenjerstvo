import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { Auth } from '../../service/auth';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, MatInputModule, MatButtonModule, MatCardModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  constructor(private authService: Auth) {}
  email = '';
  originalUrl = '';
  resultUrl: string | null = null;

  errorMessage = '';
  loading = false;

  login() {
    this.errorMessage = '';
    this.loading = true;
    this.authService.login(this.email).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (err) => {
        this.loading = false;
        if (err.status === 404) {
          this.errorMessage = 'Short URL not found';
        } else if (err.status === 400) {
          this.errorMessage = 'Invalid short URL';
        } else {
          this.errorMessage = 'Something went wrong. Please try again.';
        }
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
}
