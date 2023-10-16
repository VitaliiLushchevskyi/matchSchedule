import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from 'src/app/services/team.service';
import { Team } from 'src/app/shared/team';
import { AddPlayerDialogComponent } from './add-player-dialog/add-player-dialog.component';
import { PlayerService } from 'src/app/services/player.service';
import { Player } from 'src/app/shared/player';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-team-page',
  templateUrl: './team-page.component.html',
  styleUrls: ['./team-page.component.css'],
})
export class TeamPageComponent implements OnInit {
  team: Team;
  teamId: string;
  freePlayers: Player[] = [];
  constructor(
    private activatedRoute: ActivatedRoute,
    private service: TeamService,
    private dialog: MatDialog,
    private playerService: PlayerService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    let teamId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.service.loadTeamById(teamId).subscribe((data) => {
      this.team = data;
      this.teamId = data.id;
    });

    this.playerService.loadFreePlayers().subscribe((data) => {
      this.freePlayers = data;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddPlayerDialogComponent, {
      width: '70%',
      data: { freePlayers: this.freePlayers, teamId: this.teamId },
    });
    console.log(this.freePlayers);
    // Логіка після закриття діалогового вікна
    dialogRef.afterClosed().subscribe((result) => {});
  }
}
