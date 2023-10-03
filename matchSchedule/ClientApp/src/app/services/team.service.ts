import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Team } from '../shared/team';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  private baseUrl: string = 'https://localhost:7145/api/Team/';
  constructor(private http: HttpClient) {}

  loadTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.baseUrl}teams`);
  }

  loadTeamById(id: string): Observable<Team> {
    return this.http.get<Team>(`${this.baseUrl}` + id);
  }
}
