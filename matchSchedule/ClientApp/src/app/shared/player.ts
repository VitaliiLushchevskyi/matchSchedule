import { playerTeamHistory } from './playerTeamHistory';
import { Team } from './team';

export class Player {
  playerId: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  country: string;
  height: number | null;
  weight: number | null;
  teamId: string;
  team: Team | null;
  position: string;
  jerseyNumber: number | null;
  teamHistory: playerTeamHistory[] | null;
}
