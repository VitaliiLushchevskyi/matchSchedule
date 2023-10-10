import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl: string = 'https://localhost:7145/api/User/';
  private userPayload: any;
  constructor(private http: HttpClient, private router: Router) {
    this.userPayload = this.decodedToken();
  }

  signUp(userObj: any) {
    return this.http.post<any>(`${this.baseUrl}register`, userObj);
  }

  signOut() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }

  login(loginObj: any) {
    return this.http.post<any>(`${this.baseUrl}authenticate`, loginObj);
  }

  storeToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getFullNameFromToken() {
    if (this.userPayload) return this.userPayload.name;
  }

  getRoleFromToken() {
    if (this.userPayload) return this.userPayload.role;
  }
  isAdmin(): boolean {
    return !!(this.getRoleFromToken()?.toLowerCase() === 'admin');
  }
}
