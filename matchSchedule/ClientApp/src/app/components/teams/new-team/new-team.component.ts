import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { PlayerService } from 'src/app/services/player.service';
import { TeamService } from 'src/app/services/team.service';
import { Coach } from 'src/app/shared/coach';
import { Player } from 'src/app/shared/player';
import { Team } from 'src/app/shared/team';

@Component({
  selector: 'app-new-team',
  templateUrl: './new-team.component.html',
  styleUrls: ['./new-team.component.css'],
})
export class NewTeamComponent implements OnInit {
  teamForm: FormGroup;
  players: Player[] = [];
  constructor(
    private service: TeamService,
    private toast: NgToastService,
    private fb: FormBuilder,
    private playerService: PlayerService
  ) {
    this.teamForm = this.fb.group({
      id: [null],
      name: [null],
      country: [null],
      yearFounded: [null],
      logo: [null],
      playerIds: fb.control([]),
      // coachesIds: fb.control([]),
    });
  }

  ngOnInit(): void {
    this.playerService.loadPlayers().subscribe((data) => {
      this.players = data;
    });
  }

  onSubmit() {
    console.log(this.teamForm.value);
    this.service.createTeam(this.teamForm.value).subscribe({
      next: (res) => {
        this.toast.success({
          detail: 'SUCCESS',
          summary: res.message,
          duration: 5000,
        });
        this.teamForm.reset();
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

  isPlayerSelected(player: Player): boolean {
    const selectedPlayers = this.teamForm.get('playerIds')!.value;
    return selectedPlayers && selectedPlayers.indexOf(player) > -1;
  }

  // isCoachSelected(coach: Coach): boolean {
  //   const selectedCoaches = this.teamForm.get('coachIds')!.value;
  //   return selectedCoaches && selectedCoaches.indexOf(coach) > -1;
  // }
}
