import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsComponent } from './teams/teams.component';
import { TeamService } from '../services/team.service';



@NgModule({
  declarations: [TeamsComponent],
  imports: [
    CommonModule
  ]
  ,
  providers:[TeamService]
})
export class PageModule { }
