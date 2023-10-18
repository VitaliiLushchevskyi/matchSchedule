import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { MatchService } from 'src/app/services/match.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Tournament } from 'src/app/shared/tournament';
import { NgxMatDatetimepicker } from '@angular-material-components/datetime-picker';

@Component({
  selector: 'app-add-match-to-the-tournament-dialog',
  templateUrl: './add-match-to-the-tournament-dialog.component.html',
  styleUrls: ['./add-match-to-the-tournament-dialog.component.css'],
})
export class AddMatchToTheTournamentDialogComponent implements OnInit {
  tournament: Tournament;
  matchForm: FormGroup;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<AddMatchToTheTournamentDialogComponent>,
    private service: MatchService,
    private toast: NgToastService,
    private fb: FormBuilder
  ) {
    this.matchForm = this.fb.group({
      homeTeamId: [''],
      homeTeam: [null],
      awayTeamId: [''],
      awayTeam: [null],
      matchDateTime: [''],
      tournament: [null],
      // homeTeamGoals: [0],
      // awayTeamGoals: [0],
      referee: [''],
      // matchStatus: [''],
    });
  }
  ngOnInit(): void {
    this.tournament = this.data;
  }

  onTeamSelected(teamType: string, event: Event) {
    const selectedTeamId = (event.target as HTMLSelectElement).value;
    const selectedTeam = this.tournament.teams.find(
      (team) => team.id === selectedTeamId
    );
    if (selectedTeam) {
      this.matchForm.patchValue({
        [teamType]: selectedTeam, // встановлюємо homeTeam або awayTeam в залежності від teamType
        [teamType + 'Id']: selectedTeam.id, // встановлюємо homeTeamId або awayTeamId в залежності від teamType
      });
    }
  }

  onSubmit() {
    this.matchForm.patchValue({ ['tournament']: this.tournament });
    this.service.addMatch(this.matchForm.value).subscribe({
      next: (res) => {
        this.toast.success({
          detail: 'SUCCESS',
          summary: 'Done!',
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
    this.dialogRef.close();
  }
}
