import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { NgToastService } from 'ng-angular-popup';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-new-player-dialog',
  templateUrl: './new-player-dialog.component.html',
  styleUrls: ['./new-player-dialog.component.css'],
})
export class NewPlayerDialogComponent implements OnInit {
  playerForm: FormGroup;
  teams: Team[] = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<NewPlayerDialogComponent>,
    private service: PlayerService,
    private toast: NgToastService,
    private fb: FormBuilder,
    private teamService: TeamService
  ) {
    this.playerForm = this.fb.group({
      firstName: [''],
      lastName: [''],
      dateOfbirth: new Date(),
      country: [''],
      height: [''],
      weight: [''],
      position: [''],
      jerseyNumber: [],
      teamId: [''],
    });
  }

  ngOnInit(): void {
    this.teamService.loadTeams().subscribe((_data) => {
      this.teams = _data;
    });
  }

  onSubmit() {
    this.service.createPlayer(this.playerForm.value).subscribe({
      next: (res) => {
        this.toast.success({
          detail: 'SUCCESS',
          summary: 'Done!',
          duration: 5000,
        });
        this.playerForm.reset();
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
