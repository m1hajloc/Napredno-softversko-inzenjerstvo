import { Component, OnInit } from '@angular/core';
import { UrlShortener } from '../../service/url-shortener';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatOption, MatSelect } from '@angular/material/select';

@Component({
  selector: 'app-manage-urls',
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelect,
    MatOption,
  ],
  templateUrl: './manage-urls.html',
  styleUrl: './manage-urls.css',
})
export class ManageUrls implements OnInit {
  shortCode = '';
  originalUrl = '';
  resultUrl: string | null = null;

  constructor(private urlEntityService: UrlShortener) {}
  public entities: UrlEntity[] = [];
  public selectedEntityId?: number;

  ngOnInit(): void {
    this.urlEntityService.getUrlEntities().subscribe((entities) => {
      this.entities = entities;
    });
  }
  changeId(id: number) {
    this.selectedEntityId = id;
    let entity = this.entities.find((entity) => entity.id === id);
    if (entity) {
      this.shortCode = entity.shortUrl;
      this.originalUrl = entity.originalUrl;
    }
  }
  save() {
    let entity: UrlEntity = {
      id: this.selectedEntityId,
      shortUrl: this.shortCode,
      originalUrl: this.originalUrl,
    };
    this.urlEntityService.putUrlEntity(entity).subscribe(
      (updatedEntity) => {
        const index = this.entities.findIndex((e) => e.id === updatedEntity.id);

        if (index !== -1) {
          this.entities[index] = updatedEntity;
        }
      },
      (error) => {
        console.log(error);
      },
    );
  }
}
