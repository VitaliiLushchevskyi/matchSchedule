import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { MatchService } from 'src/app/services/match.service';
import { TeamService } from 'src/app/services/team.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Team } from 'src/app/shared/team';
import { Tournament } from 'src/app/shared/tournament';

@Component({
  selector: 'app-add-match',
  templateUrl: './add-match.component.html',
  styleUrls: ['./add-match.component.css'],
})
export class AddMatchComponent {
  matchForm: FormGroup;
  teams: Team[] = [];
  tournaments: Tournament[] = []; // Загрузка турнірів для вибору з форми

  constructor(
    private fb: FormBuilder,
    private toast: NgToastService,
    private matchService: MatchService,
    private tournamentsService: TournamentService,
    private teamService: TeamService
  ) {
    this.matchForm = this.fb.group({
      homeTeamId: [''],
      homeTeam: [null],
      awayTeamId: [''],
      awayTeam: [null],
      matchDateTime: [''],
      tournament: [null], // Вибір турніру
      // homeTeamGoals: [0],
      // awayTeamGoals: [0],
      referee: [''],
      // matchStatus: [''],
    });
  }

  ngOnInit(): void {
    // Завантаження турнірів під час ініціалізації компонента
    this.loadTournaments();
    this.loadTeams();
  }

  loadTeams() {
    this.teamService.loadTeams().subscribe((data) => {
      this.teams = data;
    });
  }

  loadTournaments() {
    this.tournamentsService.loadTournaments().subscribe((data) => {
      this.tournaments = data;
    });
  }

  onTeamSelected(teamType: string, event: Event) {
    const selectedTeamId = (event.target as HTMLSelectElement).value;
    const selectedTeam = this.teams.find((team) => team.id === selectedTeamId);
    if (selectedTeam) {
      this.matchForm.patchValue({
        [teamType]: selectedTeam, // встановлюємо homeTeam або awayTeam в залежності від teamType
        [teamType + 'Id']: selectedTeam.id, // встановлюємо homeTeamId або awayTeamId в залежності від teamType
      });
    }
  }

  onSubmit() {
    this.matchService.addMatch(this.matchForm.value).subscribe({
      next: (res) => {
        this.toast.success({
          detail: 'SUCCESS',
          // summary: res.message,
          duration: 5000,
        });
        this.matchForm.reset();
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
