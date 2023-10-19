import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsComponent } from './teams/teams.component';
import { TeamService } from '../services/team.service';
import { TournamentsListComponent } from './tournaments-list/tournaments-list.component';
import { TournamentService } from '../services/tournament.service';
import { RouterModule, Routes } from '@angular/router';
import { MatExpansionModule } from '@angular/material/expansion';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TeamPageComponent } from './team-page/team-page.component';
import { PlayerInfoComponent } from './players-list/player-info/player-info.component';
import {
  NgxMatDatetimePickerModule,
  NgxMatTimepickerModule,
} from '@angular-material-components/datetime-picker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { TeamPlayersListComponent } from './team-page/team-players-list/team-players-list.component';
import { CalculateAgePipe } from '../pipes/age.pipe';
import { NewTournamentComponent } from './tournaments-list/new-tournament/new-tournament.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { NewTeamComponent } from './teams/new-team/new-team.component';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AuthGuard } from '../guards/auth.guard';
import { AdminGuard } from '../guards/admin.guard';
import { AddPlayerDialogComponent } from './team-page/add-player-dialog/add-player-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from '../interceptors/token.interceptor';
import { AddMatchToTheTournamentDialogComponent } from './tournaments-list/add-match-to-the-tournament-dialog/add-match-to-the-tournament-dialog.component';
import { EditTournamentComponent } from './tournaments-list/edit-tournament-dialog/edit-tournament.component';
import { PlayersListComponent } from './players-list/players-list.component';
import { PlayerService } from '../services/player.service';
import { NewPlayerDialogComponent } from './players-list/new-player-dialog/new-player-dialog.component';

const routes: Routes = [
  { path: 'teams', component: TeamsComponent },
  { path: 'teams/new', component: NewTeamComponent, canActivate: [AdminGuard] },
  { path: 'teams/:id', component: TeamPageComponent },
  { path: 'players', component: PlayersListComponent },
  { path: 'players/:id', component: PlayerInfoComponent },
  { path: 'tournaments', component: TournamentsListComponent },
  {
    path: 'tournaments/new',
    component: NewTournamentComponent,
    canActivate: [AdminGuard],
  },
];

@NgModule({
  declarations: [
    TeamsComponent,
    TournamentsListComponent,
    EditTournamentComponent,
    TeamPageComponent,
    PlayerInfoComponent,
    TeamPlayersListComponent,
    CalculateAgePipe,
    NewTournamentComponent,
    NewTeamComponent,
    AddPlayerDialogComponent,
    AddMatchToTheTournamentDialogComponent,
    PlayersListComponent,
    NewPlayerDialogComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    MatExpansionModule,
    ReactiveFormsModule,
    MatListModule,
    FormsModule,
    NgSelectModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
  ],
  providers: [TeamService, TournamentService, PlayerService],
})
export class PageModule {}
