import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';
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
  columnsToDisplay = ['logo', 'name', 'country', 'year', 'delete'];
  filterValues = {
    name: '',
    country: '',
  };
  teams: Team[] = [];
  countries: string[];
  selectedCountry: string;
  countryByDefault: string;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    private teamService: TeamService,
    public authService: AuthService,
    private toast: NgToastService
  ) {}

  ngOnInit(): void {
    this.countryByDefault = 'All';
    this.nameFilter.valueChanges.subscribe((name) => {
      this.filterValues.name = name ? name.toLowerCase() : '';
      this.dataSource.filter = JSON.stringify(this.filterValues);
    });

    this.teamService.loadTeams().subscribe((_teams) => {
      this.teams = _teams;
      this.dataSource.data = this.teams;
      this.dataSource.filterPredicate = this.createFilter();
      this.dataSource.paginator = this.paginator;
    });
    this.countries = this.teamService.countries;
    if (!this.countries.includes(this.countryByDefault)) {
      this.countries.push(this.countryByDefault);
      this.countries.sort();
    }
    this.countries.sort();
    // this.selectedCountry = this.countryByDefault;
  }

  createFilter(): (data: any, filter: string) => boolean {
    let filterFunction = (data: any, filter: string): boolean => {
      let searchTerms = JSON.parse(filter.toLowerCase());
      let isNameMatch = data.name.toLowerCase().includes(searchTerms.name);
      let isCountryMatch =
        searchTerms.country === '' ||
        data.country.toLowerCase() === searchTerms.country;

      let isDefaultCountry =
        searchTerms.country.trim().toLowerCase() ===
        this.countryByDefault.toLowerCase();

      // Якщо === all то відобразити всі
      if (isDefaultCountry) {
        return isNameMatch;
      }
      return isNameMatch && isCountryMatch;
    };
    return filterFunction;
  }

  onCountryChange() {
    this.filterValues.country = this.selectedCountry.toLowerCase();
    this.dataSource.filter = JSON.stringify(this.filterValues);
  }

  onDelete(id: string) {
    this.teamService.deleteTeam(id).subscribe({
      next: (res) => {
        this.teams = this.teams.filter((team: Team) => team.id !== id);
        this.dataSource.data = this.teams;

        this.toast.success({
          detail: 'SUCCESS',
          summary: res.message,
          duration: 5000,
        });
      },
      error: (err) => {
        this.toast.error({
          detail: 'ERROR',
          summary: err.error.message,
          duration: 5000,
        });
      },
    });
  }
}
