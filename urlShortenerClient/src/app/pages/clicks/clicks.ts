import { Component } from '@angular/core';
import { UrlShortener } from '../../service/url-shortener';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { MatTable } from '@angular/material/table';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'app-clicks',
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelect,
    MatOption,
    MatList,
    MatListItem,
    MatTable,
  ],
  templateUrl: './clicks.html',
  styleUrl: './clicks.css',
})
export class Clicks {
  constructor(private urlEntityService: UrlShortener) {}

  shortCode = '';
  originalUrl = '';
  resultUrl: string | null = null;

  errorMessage = '';
  loading = false;

  public entities: UrlEntity[] = [];
  public entitiesClicks: UrlEntityClick[] = [];
  public selectedEntityId: number = 0;

  ngOnInit(): void {
    this.urlEntityService.getUrlEntities().subscribe((entities) => {
      this.entities = entities;
    });
  }
  changeId(id: number) {
    this.urlEntityService.getUrlEntityClicks(id).subscribe((entitiesClicks) => {
      this.selectedEntityId = id;
      this.entitiesClicks = entitiesClicks;
      console.log(entitiesClicks);
      console.log(this.entitiesClicks);
    });
  }

  onClick(id: number) {
    this.urlEntityService.getUrlEntityClicks(id).subscribe((entitiesClicks) => {
      this.selectedEntityId = id;
      this.entitiesClicks = entitiesClicks;
    });
  }
}
