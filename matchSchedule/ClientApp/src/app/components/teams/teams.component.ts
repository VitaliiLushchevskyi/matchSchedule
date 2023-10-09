import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { TeamService } from 'src/app/services/team.service';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css'],
})
export class TeamsComponent implements OnInit {
  nameFilter = new FormControl('');
  dataSource = new MatTableDataSource();
  columnsToDisplay = ['logo', 'name', 'country', 'year'];
  filterValues = {
    name: '',
  };
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private teamService: TeamService) {}

  ngOnInit(): void {
    this.nameFilter.valueChanges.subscribe((name) => {
      this.filterValues.name = name!;
      this.dataSource.filter = JSON.stringify(this.filterValues);
    });
    this.teamService.loadTeams().subscribe((_teams) => {
      this.dataSource.data = _teams;
      this.dataSource.filterPredicate = this.createFilter();
      this.dataSource.paginator = this.paginator; // Призначити пагінатор джерелу даних
    });
  }

  createFilter(): (data: any, filter: string) => boolean {
    let filterFunction = function (data: any, filter: string): boolean {
      let searchTerms = JSON.parse(filter.toLowerCase());
      return data.name.toLowerCase().indexOf(searchTerms.name) !== -1;
    };
    return filterFunction;
  }
}
