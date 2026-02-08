import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { UrlShortener } from '../../service/url-shortener';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule, MatInputModule, MatButtonModule, MatCardModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  constructor(private urlService: UrlShortener) {}
  shortCode = '';
  originalUrl = '';
  resultUrl: string | null = null;

  errorMessage = '';
  loading = false;

  redirect() {
    this.errorMessage = '';
    this.loading = true;
    this.urlService.resolveShortUrl(this.shortCode).subscribe({
      next: (targetUrl) => {
        console.log(targetUrl);
        window.location.href = targetUrl;
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
