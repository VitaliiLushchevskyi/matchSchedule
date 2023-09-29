import { Match } from './match';
import { Team } from './team';

export class Tournament {
  id: string;
  name: string;
  location: string;
  startDate: string;
  endDate: string;
  description: string;
  matches: Match[];
  teams: Team[];
}
