import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Match } from '../shared/match';

@Injectable({
  providedIn: 'root',
})
export class MatchService {
  baseUrl = 'https://localhost:7145/api/Match/';
  constructor(private http: HttpClient) {}

  addMatch(model: Match) {
    return this.http
      .post<Match>(`${this.baseUrl}createMatch`, model)
      .pipe
      //smth
      ();
  }
}
