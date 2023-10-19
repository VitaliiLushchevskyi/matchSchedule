import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDateFormats } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NgToastService } from 'ng-angular-popup';
import { PlayerService } from 'src/app/services/player.service';
import { Player } from 'src/app/shared/player';
import { NewPlayerDialogComponent } from './new-player-dialog/new-player-dialog.component';

@Component({
  selector: 'app-players-list',
  templateUrl: './players-list.component.html',
  styleUrls: ['./players-list.component.css'],
})
export class PlayersListComponent implements OnInit {
  nameFilter = new FormControl('');
  players: Player[] = [];
  dataSource: any;
  columnsToDisplay = ['photo', 'name', 'country', 'team', 'age', 'position'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    private service: PlayerService,
    private toast: NgToastService,
    private _liveAnnouncer: LiveAnnouncer,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.service.loadPlayers().subscribe((data) => {
      this.players = data;
      if (this.players) {
        this.players.forEach((player: Player) => {
          player.age = this.service.calculateAge(player.dateOfBirth);
          player.teamName = player.team!.name;
        });
      }

      this.dataSource = new MatTableDataSource(this.players);
      this.dataSource.sort = this.sort;
      console.log(this.dataSource);
      this.dataSource.paginator = this.paginator;
      this.dataSource.filterPredicate = this.createFilter(
        'fullName',
        'team.name'
      );
    });
  }

  openAddDialog() {
    const dialogRef = this.dialog.open(NewPlayerDialogComponent, {
      width: '80%',
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue;
  }

  createFilter(
    fullName: string,
    team: string
  ): (data: any, filter: string) => boolean {
    return (data: any, filter: string) => {
      const player = data;
      const fieldValue = player[fullName].toLowerCase();
      return fieldValue.includes(filter.toLowerCase());
      // const fullName = `${player.firstName} ${player.lastName}`.toLowerCase();
      // return fullName.includes(filter.toLowerCase());
    };
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
