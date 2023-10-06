import { Coach } from './coach';
import { Match } from './match';
import { Player } from './player';
import { Tournament } from './tournament';

export class Team {
  id: string;
  name: string;
  country: string;
  yearFounded: number | null;
  logo: string;
  players: Player[];
  coaches: Coach[];
  matches: Match[];
  tournamentsWon: Tournament[];
}
