import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsComponent } from './teams/teams.component';
import { TeamService } from '../services/team.service';
import { TournamentsListComponent } from './tournaments-list/tournaments-list.component';
import { TournamentService } from '../services/tournament.service';
import { RouterModule, Routes } from '@angular/router';
import { MatExpansionModule } from '@angular/material/expansion';
import { AddMatchComponent } from './add-match/add-match.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TeamPageComponent } from './team-page/team-page.component';
import { PlayerInfoComponent } from './player-info/player-info.component';
import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker';

const routes: Routes = [
  { path: 'teams', component: TeamsComponent },
  { path: 'teams/:id', component: TeamPageComponent },
  { path: 'players/:id', component: PlayerInfoComponent },
  { path: 'tournaments', component: TournamentsListComponent },
  { path: 'matches/new', component: AddMatchComponent },
];

@NgModule({
  declarations: [
    TeamsComponent,
    TournamentsListComponent,
    AddMatchComponent,
    TeamPageComponent,
    PlayerInfoComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    MatExpansionModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgxMatDatetimePickerModule
  ],
  providers: [TeamService, TournamentService],
})
export class PageModule {}
