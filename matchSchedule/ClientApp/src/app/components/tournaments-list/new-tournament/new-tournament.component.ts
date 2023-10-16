import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { TeamService } from 'src/app/services/team.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Team } from 'src/app/shared/team';
import { Tournament } from 'src/app/shared/tournament';

@Component({
  selector: 'app-new-tournament',
  templateUrl: './new-tournament.component.html',
  styleUrls: ['./new-tournament.component.css'],
})
export class NewTournamentComponent implements OnInit {
  tournament: Tournament;
  tournamentForm: FormGroup;
  teams: Team[] = [];
  constructor(
    private tournamentService: TournamentService,
    private fb: FormBuilder,
    private toast: NgToastService,
    private teamService: TeamService
  ) {
    this.tournamentForm = this.fb.group({
      id: [null],
      name: [null],
      location: [null],
      startDate: new Date(),
      endDate: new Date(),
      description: [null],
      teamIds: fb.control([]),
    });
  }

  ngOnInit(): void {
    this.teamService.loadTeams().subscribe((data) => {
      this.teams = data;
    });
  }

  isSelected(team: Team): boolean {
    const selectedTeams = this.tournamentForm.get('teamIds')!.value;
    return selectedTeams && selectedTeams.indexOf(team) > -1;
  }

  onSubmit() {
    console.log(this.tournamentForm.value);
    this.tournamentService
      .createTournament(this.tournamentForm.value)
      .subscribe({
        next: (res) => {
          this.toast.success({
            detail: 'SUCCESS',
            summary: res.message,
            duration: 5000,
          });
          this.tournamentForm.reset();
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
