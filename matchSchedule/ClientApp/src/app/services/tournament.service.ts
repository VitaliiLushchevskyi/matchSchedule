import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament } from '../shared/tournament';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  private baseUrl: string = 'https://localhost:7145/api/Tournament';
  constructor(private http: HttpClient) {}

  loadTournaments(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(`${this.baseUrl}/tournaments`);
  }
}