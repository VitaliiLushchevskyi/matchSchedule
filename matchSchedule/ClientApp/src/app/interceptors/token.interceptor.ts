import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private toast: NgToastService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const myToken = this.authService.getToken();

    if (myToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${myToken}`,
          // 'Content-Type': 'application/json',
        },
      });
    }

    return next.handle(request).pipe(
      catchError((err: any) => {
        console.log(err);
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.toast.warning({
              detail: 'Warning!',
              summary: 'Token is expired, Please login again!',
            });
            this.router.navigate(['login']);
          }
        }
        return throwError(() => err);
      })
    );
  }
}
