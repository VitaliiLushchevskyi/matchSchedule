import { Match } from "./match";

export class TournamentViewModel {
    id: string;
    name: string;
    location: string;
    startDate: string;
    endDate: string;
    description: string;
    matches: Match[] = [];
    teamIds: string[];
  }
  