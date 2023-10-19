import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from 'src/app/services/team.service';
import { Player } from 'src/app/shared/player';
import { Team } from 'src/app/shared/team';
import { AddPlayerDialogComponent } from '../add-player-dialog/add-player-dialog.component';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'app-team-players-list',
  templateUrl: './team-players-list.component.html',
  styleUrls: ['./team-players-list.component.css'],
})
export class TeamPlayersListComponent implements OnInit {
  team: Team;
  players: Player[];
  dataSource: any;

  displayedColumns: string[] = [
    'jerseyNumber',
    'name',
    'age',
    'country',
    'position',
    'height',
    'weight',
  ];

  constructor(
    private _liveAnnouncer: LiveAnnouncer,
    private teamService: TeamService,
    private playerService: PlayerService,
    private activatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    let teamId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.teamService.loadTeamById(teamId).subscribe((data) => {
      this.team = data;
      this.players = data.players;
      if (this.team && this.team.players) {
        this.players.forEach((player: Player) => {
          player.age = this.playerService.calculateAge(player.dateOfBirth);
        });
      }

      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
    });
  }

  @ViewChild(MatSort) sort: MatSort;

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}
