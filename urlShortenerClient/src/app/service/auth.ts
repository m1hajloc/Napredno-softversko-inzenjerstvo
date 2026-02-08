import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  API_URL = 'https://localhost:7237/getUser';

  private readonly API_KEY_STORAGE = 'apiKey';

  private loggedInSubject = new BehaviorSubject<boolean>(
    !!localStorage.getItem(this.API_KEY_STORAGE),
  );

  isLoggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(email: string): Observable<User> {
    return this.http.post<User>(this.API_URL, { email }).pipe(
      tap((response) => {
        this.saveApiKey(response.apiKey);
        this.loggedInSubject.next(true);
      }),
    );
  }

  logout(): void {
    localStorage.removeItem(this.API_KEY_STORAGE);
    this.loggedInSubject.next(false);
  }

  isLoggedIn(): boolean {
    return !!this.getApiKey();
  }

  getApiKey(): string | null {
    return localStorage.getItem(this.API_KEY_STORAGE);
  }

  private saveApiKey(apiKey: string): void {
    localStorage.setItem(this.API_KEY_STORAGE, apiKey);
  }
}
