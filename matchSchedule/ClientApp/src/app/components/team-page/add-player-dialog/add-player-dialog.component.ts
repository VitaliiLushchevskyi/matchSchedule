import { Component } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Inject } from '@angular/core';
import { Player } from 'src/app/shared/player';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-add-player-dialog',
  templateUrl: './add-player-dialog.component.html',
  styleUrls: ['./add-player-dialog.component.css'],
})
export class AddPlayerDialogComponent {
  players: Player[] = [];
  selectedPlayersIds: string[] = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<AddPlayerDialogComponent>,
    private service: TeamService
  ) {
    this.players = data.freePlayers;
  }
  onSubmit() {
    this.service
      .addListOfPlayersToTeam(this.data.teamId, this.selectedPlayersIds)
      .subscribe({
        next: (res) => {},
        error: (err) => {},
      });
    this.dialogRef.close();
  }
}
