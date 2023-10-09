import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament } from '../shared/tournament';
import { TournamentViewModel } from '../shared/tournamentViewModel';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  private baseUrl: string = 'https://localhost:7145/api/Tournament';
  constructor(private http: HttpClient) {}

  loadTournaments(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(`${this.baseUrl}/tournaments`);
  }

  createTournament(model: TournamentViewModel): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/new`, model);
  }

  deleteTournament(id: string) {
    return this.http.delete(`${this.baseUrl}/` + id);
  }
}
