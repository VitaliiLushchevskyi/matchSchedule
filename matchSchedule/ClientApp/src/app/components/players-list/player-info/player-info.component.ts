import { Component, OnInit } from '@angular/core';
import { MatDateFormats, NativeDateAdapter } from '@angular/material/core';
import { ActivatedRoute } from '@angular/router';
import { PlayerService } from 'src/app/services/player.service';
import { Player } from 'src/app/shared/player';

@Component({
  selector: 'app-player-info',
  templateUrl: './player-info.component.html',
  styleUrls: ['./player-info.component.css'],
})
export class PlayerInfoComponent implements OnInit {
  player: Player;
  constructor(
    private service: PlayerService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let playerId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.service.loadPlayerById(playerId).subscribe((data) => {
      this.player = data;
    });
  }
}
