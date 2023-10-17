import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Match } from 'src/app/shared/match';
import { Tournament } from 'src/app/shared/tournament';
import { AddMatchToTheTournamentDialogComponent } from './add-match-to-the-tournament-dialog/add-match-to-the-tournament-dialog.component';
import { EditTournamentComponent } from './edit-tournament-dialog/edit-tournament.component';

@Component({
  selector: 'app-tournaments-list',
  templateUrl: './tournaments-list.component.html',
  styleUrls: ['./tournaments-list.component.css'],
})
export class TournamentsListComponent implements OnInit {
  tournaments: Tournament[] = [];
  tournament: Tournament;

  constructor(
    private service: TournamentService,
    public authService: AuthService,
    private toast: NgToastService,
    private dialog: MatDialog
  ) {}
  panelOpenState = false;

  ngOnInit(): void {
    this.service.loadTournaments().subscribe((data) => {
      this.tournaments = data;
    });
  }

  openEditDialog(id: string): void {
    this.service.loadTournamentById(id).subscribe((data) => {
      this.tournament = data;
      const dialogRef = this.dialog.open(EditTournamentComponent, {
        width: '80%',
        data: this.tournament,
      });

      dialogRef.afterClosed().subscribe((result) => {
        this.service.loadTournaments().subscribe((data) => {
          this.tournaments = data;
        });
      });
    });
  }

  openNewMatchDialog(id: string): void {
    this.service.loadTournamentById(id).subscribe((data) => {
      this.tournament = data;
      const dialogRef = this.dialog.open(
        AddMatchToTheTournamentDialogComponent,
        {
          width: '70%',
          data: this.tournament,
        }
      );
    });
  }

  onDelete(id: string) {
    this.service.deleteTournament(id).subscribe({
      next: (res) => {
        this.tournaments = this.tournaments.filter(
          (tournament) => tournament.id !== id
        );
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
