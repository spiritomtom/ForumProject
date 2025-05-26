import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = 'https://localhost:5001/api/auth';

  constructor(private http: HttpClient) {}

  login(data: any) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }

  register(data: any) {
    return this.http.post(`${this.baseUrl}/register`, data);
  }

  logout() {
    return this.http.post(`${this.baseUrl}/logout`, {});
  }

  me() {
    return this.http.get(`${this.baseUrl}/me`);
  }
}
