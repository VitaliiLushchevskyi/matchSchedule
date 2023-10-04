import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from 'src/app/services/team.service';
import { Player } from 'src/app/shared/player';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-team-players-list',
  templateUrl: './team-players-list.component.html',
  styleUrls: ['./team-players-list.component.css'],
})
export class TeamPlayersListComponent implements OnInit {
  team: Team;
  players: Player[];
  dataSource: any;

  displayedColumns: string[] = ['jerseyNumber', 'name', 'age', 'country','position' , 'height','weight'];

  constructor(
    private _liveAnnouncer: LiveAnnouncer,
    private service: TeamService,
    private activatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    let teamId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.service.loadTeamById(teamId).subscribe((data) => {
      this.team = data;
      this.players = data.players;
      this.dataSource = new MatTableDataSource(this.team.players);
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

  // calculateAge(birthDate: string): number {
  //   const _birthDate = new Date(birthDate);
  //   const today = new Date();
  //   const birthYear = _birthDate.getFullYear();
  //   const currentYear = today.getFullYear();
  //   let age = currentYear - birthYear;

  //   const birthMonth = _birthDate.getMonth();
  //   const currentMonth = today.getMonth();

  //   if (
  //     currentMonth < birthMonth ||
  //     (currentMonth === birthMonth && today.getDate() < _birthDate.getDate())
  //   ) {
  //     age--;
  //   }

  //   return age;
  // }
}
