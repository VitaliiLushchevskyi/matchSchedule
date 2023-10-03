import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Player } from '../shared/player';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private baseUrl: string = 'https://localhost:7145/api/Player/';
  constructor(private http: HttpClient) {}

  loadPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.baseUrl}players`);
  }

  loadPlayerById(id: string): Observable<Player> {
    return this.http.get<Player>(`${this.baseUrl}` + id);
  }
}
