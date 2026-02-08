import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { UrlShortener } from '../../service/url-shortener';

@Component({
  selector: 'app-create-url',
  imports: [CommonModule, FormsModule, MatInputModule, MatButtonModule, MatCardModule],
  templateUrl: './create-url.html',
  styleUrl: './create-url.css',
})
export class CreateUrl {
  shortCode = '';
  originalUrl = '';
  resultUrl: string | null = null;
  constructor(private urlEntityService: UrlShortener) {}

  go() {
    let entity: UrlEntity = {
      shortUrl: this.shortCode,
      originalUrl: this.originalUrl,
    };
    this.urlEntityService.postUrlEntity(entity).subscribe(
      (entity) => {
        console.log('Successfully created UrlEntity!', entity);
      },
      (error) => {
        console.log(error);
      },
    );
  }
}
