import { Tournament } from './tournament';

export class Match {
  matchId: string;
  homeTeamId: string;
  awayTeamId: string;
  matchDateTime: string;
  tournament: Tournament | null;
  homeTeamGoals: number = 0;
  awayTeamGoals: number = 0;
  referee: string;
  matchStatus: string;
}
