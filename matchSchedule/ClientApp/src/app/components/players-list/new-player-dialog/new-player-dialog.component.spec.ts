import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPlayerDialogComponent } from './new-player-dialog.component';

describe('NewPlayerDialogComponent', () => {
  let component: NewPlayerDialogComponent;
  let fixture: ComponentFixture<NewPlayerDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewPlayerDialogComponent]
    });
    fixture = TestBed.createComponent(NewPlayerDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
