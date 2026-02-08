import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Auth } from './auth';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UrlShortener {
  private apiUrl = 'https://localhost:7237/';
  public urlEntityClicks: UrlEntityClick[] = [];
  public urlEntities?: UrlEntity[];

  constructor(
    private http: HttpClient,
    private authService: Auth,
  ) {}

  // getEntities(){
  //   return this.urlEntities ??
  // }

  resolveShortUrl(shortUrl: string) {
    return this.http.get<string>(`${this.apiUrl}getOriginalUrl/${shortUrl}`);
  }

  getUrlEntityClicks(urlEntityId: number) {
    console.log(`${this.apiUrl}GetUrlEntityClicks/${urlEntityId}`);
    return this.http.get<UrlEntityClick[]>(`${this.apiUrl}GetUrlEntityClicks/${urlEntityId}`);
  }

  getUrlEntities() {
    return this.http.get<UrlEntity[]>(`${this.apiUrl}GetUrlEntities`).pipe(
      tap((result) => {
        this.urlEntities = result;
      }),
    );
  }

  postUrlEntity(urlEntity: UrlEntity) {
    return this.http.post<{ urlEntity: UrlEntity }>(`${this.apiUrl}api/urls`, urlEntity).pipe(
      tap(() => {
        this.getUrlEntities();
      }),
    );
  }
  putUrlEntity(urlEntity: UrlEntity) {
    return this.http.put<UrlEntity>(`${this.apiUrl}api/urls/${urlEntity.id}`, urlEntity).pipe(
      tap(() => {
        this.getUrlEntities();
      }),
    );
  }
}
