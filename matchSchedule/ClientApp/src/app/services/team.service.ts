import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Team } from '../shared/team';
import { TeamViewModel } from '../shared/teamViewModel';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  private baseUrl: string = 'https://localhost:7145/api/Team/';
  countries: string[] = [
    'Ukraine',
    'Brazil',
    'England',
    'France',
    'Portugal',
    'Italy',
    'Poland',
    'USA',
    'Germany',
    'Spain',
    'Argentina',
  ];
  constructor(private http: HttpClient) {}

  loadTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.baseUrl}teams`);
  }

  loadTeamById(id: string): Observable<Team> {
    return this.http.get<Team>(`${this.baseUrl}` + id);
  }

  createTeam(model: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}new`, model);
  }

  addPlayerToTeam(teamId: string, playerId: string) {
    return this.http.post<any>(
      `${this.baseUrl}${teamId}/addPlayer/${playerId}`,
      null
    );
  }

  addListOfPlayersToTeam(teamId: string, playersIds: string[]) {
    return this.http.post<any>(
      `${this.baseUrl}${teamId}/addListOfPlayers`,
      playersIds
    );
  }
}
