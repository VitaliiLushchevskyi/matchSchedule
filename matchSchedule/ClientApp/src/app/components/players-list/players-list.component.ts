import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgToastService } from 'ng-angular-popup';
import { PlayerService } from 'src/app/services/player.service';
import { Player } from 'src/app/shared/player';

@Component({
  selector: 'app-players-list',
  templateUrl: './players-list.component.html',
  styleUrls: ['./players-list.component.css'],
})
export class PlayersListComponent implements OnInit {
  nameFilter = new FormControl('');
  players: Player[] = [];
  dataSource = new MatTableDataSource();
  columnsToDisplay = ['photo', 'name', 'country', 'team', 'age', 'position'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private service: PlayerService, private toast: NgToastService) {}

  ngOnInit(): void {
    this.service.loadPlayers().subscribe((data) => {
      this.players = data;
      this.dataSource.data = this.players;
      this.dataSource.paginator = this.paginator;
      this.dataSource.filterPredicate = this.createFilter();
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue;
  }

  createFilter(): (data: any, filter: string) => boolean {
    return (data: any, filter: string) => {
      const player = data;
      const fullName = `${player.firstName} ${player.lastName}`.toLowerCase();
      return fullName.includes(filter.toLowerCase());
    };
  }


}
