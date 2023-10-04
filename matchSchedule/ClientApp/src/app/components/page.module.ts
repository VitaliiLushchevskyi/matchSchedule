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

import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { TeamPlayersListComponent } from './team-page/team-players-list/team-players-list.component';
import { CalculateAgePipe } from '../pipes/age.pipe';

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
    TeamPlayersListComponent,
    CalculateAgePipe
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    MatExpansionModule,
    ReactiveFormsModule,
    NgSelectModule,
    NgxMatDatetimePickerModule,
    MatTableModule,
    MatSortModule
    
    
  ],
  providers: [TeamService, TournamentService],
})
export class PageModule {}
