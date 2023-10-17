import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TournamentService } from 'src/app/services/tournament.service';
import { Tournament } from 'src/app/shared/tournament';
import { TournamentEditDTO } from 'src/app/shared/tournamentEditDTO';

@Component({
  selector: 'app-edit-tournament',
  templateUrl: './edit-tournament.component.html',
  styleUrls: ['./edit-tournament.component.css'],
})
export class EditTournamentComponent {
  tournament: Tournament;
  tournamentForm: FormGroup;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditTournamentComponent>,
    private service: TournamentService,
    private fb: FormBuilder
  ) {
    this.tournament = data;
    this.tournamentForm = this.fb.group({
      name: [this.tournament.name],
      location: [this.tournament.location],
      startDate: [this.tournament.startDate],
      endDate: [this.tournament.endDate],
      description: [this.tournament.description],
    });
  }

  onSubmit() {
    const editedTournament = this.tournamentForm.value;
    this.service
      .editTournament(this.tournament.id, editedTournament)
      .subscribe({
        next: (res) => {},
        error: (err) => {},
      });
    this.dialogRef.close();
  }
}
