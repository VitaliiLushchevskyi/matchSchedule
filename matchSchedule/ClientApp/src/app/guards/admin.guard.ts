import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard {
  constructor(
    private authService: AuthService,
    private router: Router,
    private toast: NgToastService
  ) {}

  canActivate(): boolean {
    if (this.authService.getRoleFromToken()?.toLowerCase() === 'admin') {
      console.log(this.authService.getRoleFromToken());
      return true;
    } else {
      this.toast.error({ detail: 'ERROR', summary: 'Please log in as Admin!' });
      this.router.navigate(['login']);
      return false;
    }
  }
}
