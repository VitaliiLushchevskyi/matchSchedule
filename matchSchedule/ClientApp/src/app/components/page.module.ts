import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsComponent } from './teams/teams.component';
import { TeamService } from '../services/team.service';
import { TournamentsListComponent } from './tournaments-list/tournaments-list.component';
import { TournamentService } from '../services/tournament.service';
import { RouterModule, Routes } from '@angular/router';
import {MatExpansionModule} from '@angular/material/expansion';

const routes: Routes = [
  { path: 'teams', component: TeamsComponent },
  { path: 'tournaments', component: TournamentsListComponent },
];

@NgModule({
  declarations: [TeamsComponent, TournamentsListComponent],
  imports: [CommonModule, RouterModule.forRoot(routes),MatExpansionModule],
  providers: [TeamService, TournamentService],
})
export class PageModule {}
