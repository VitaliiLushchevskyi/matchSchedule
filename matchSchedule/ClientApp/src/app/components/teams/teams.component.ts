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
    country: '',
  };
  countries: string[];
  selectedCountry: string;
  countryByDefault: string = 'All';

  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(private teamService: TeamService) {}

  ngOnInit(): void {
    this.nameFilter.valueChanges.subscribe((name) => {
      this.filterValues.name = name ? name.toLowerCase() : '';

      this.dataSource.filter = JSON.stringify(this.filterValues);
    });

    this.teamService.loadTeams().subscribe((_teams) => {
      this.dataSource.data = _teams;
      this.dataSource.filterPredicate = this.createFilter();
      this.dataSource.paginator = this.paginator;
    });
    this.countries = this.teamService.countries;
    this.countries.push(this.countryByDefault);
    this.countries.sort();
    this.selectedCountry = this.countryByDefault;
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
}
