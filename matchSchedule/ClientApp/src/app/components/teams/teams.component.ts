import { Component, OnInit } from '@angular/core';
import { TeamService } from 'src/app/services/team.service';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css'],
})
export class TeamsComponent implements OnInit {
  teams: Team[] = [];
  constructor(private service: TeamService) {}

  ngOnInit(): void {
    // this.service.loadTeams().subscribe((data) => {
    //   this.teams = data;
    // });
  }
}
