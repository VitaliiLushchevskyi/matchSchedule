import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Match } from 'src/app/shared/match';
import { Tournament } from 'src/app/shared/tournament';

@Component({
  selector: 'app-tournaments-list',
  templateUrl: './tournaments-list.component.html',
  styleUrls: ['./tournaments-list.component.css'],
})
export class TournamentsListComponent implements OnInit {
  tournaments: Tournament[] = [];

  constructor(
    private service: TournamentService,
    public authService: AuthService,
    private toast: NgToastService
  ) {}
  panelOpenState = false;

  ngOnInit(): void {
    this.service.loadTournaments().subscribe((data) => {
      this.tournaments = data;
      
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
