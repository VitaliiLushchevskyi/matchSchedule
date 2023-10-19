import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Player } from '../shared/player';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  private baseUrl: string = 'https://localhost:7145/api/Player/';
  constructor(private http: HttpClient) {}

  loadPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.baseUrl}players`).pipe(
      map((players) => {
        return players.map((player) => {
          return {
            ...player,
            fullName: `${player.lastName} ${player.firstName}`.toString(),
          };
        });
      })
    );
  }

  loadPlayerById(id: string): Observable<Player> {
    return this.http.get<Player>(`${this.baseUrl}` + id);
  }

  loadFreePlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.baseUrl}players/free`);
  }

  createPlayer(model: Player): Observable<Player> {
    return this.http.post<Player>(`${this.baseUrl}createPlayer`, model);
  }

  calculateAge(birthdate: Date): number {
    const today = new Date();
    const birthDate = new Date(birthdate);
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();
    if (
      monthDiff < 0 ||
      (monthDiff === 0 && today.getDate() < birthDate.getDate())
    ) {
      age--;
    }
    return age;
  }
}
