import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from 'src/app/services/team.service';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-team-page',
  templateUrl: './team-page.component.html',
  styleUrls: ['./team-page.component.css'],
})
export class TeamPageComponent implements OnInit {
  team: Team;
  constructor(
    private activatedRoute: ActivatedRoute,
    private service: TeamService
  ) {}
  ngOnInit(): void {
    let teamId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.service.loadTeamById(teamId).subscribe((data) => {
      this.team = data;
    });
  }
}
