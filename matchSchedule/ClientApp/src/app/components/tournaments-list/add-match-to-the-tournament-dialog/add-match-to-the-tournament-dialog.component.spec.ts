import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMatchToTheTournamentDialogComponent } from './add-match-to-the-tournament-dialog.component';

describe('AddMatchToTheTournamentDialogComponent', () => {
  let component: AddMatchToTheTournamentDialogComponent;
  let fixture: ComponentFixture<AddMatchToTheTournamentDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddMatchToTheTournamentDialogComponent]
    });
    fixture = TestBed.createComponent(AddMatchToTheTournamentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
