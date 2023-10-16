import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament } from '../shared/tournament';
import { TournamentViewModel } from '../shared/tournamentViewModel';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  private baseUrl: string = 'https://localhost:7145/api/Tournament';
  constructor(private http: HttpClient, private authService: AuthService) {}

  loadTournaments(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(`${this.baseUrl}/tournaments`);
  }

  loadTournamentById(id: string): Observable<Tournament> {
    return this.http.get<Tournament>(`${this.baseUrl}/` + id);
  }

  createTournament(model: TournamentViewModel): Observable<any> {
    const token = this.authService.getToken(); // Отримайте токен з сервісу авторизації
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token?.toLowerCase()}`,
    });
    console.log(token);
    return this.http.post<any>(`${this.baseUrl}/new`, model);
  }

  deleteTournament(id: string) {
    return this.http.delete<any>(`${this.baseUrl}/` + id);
  }
}
