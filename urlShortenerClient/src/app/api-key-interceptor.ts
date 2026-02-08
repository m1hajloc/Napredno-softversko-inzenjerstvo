import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { Auth } from './service/auth';

export const apiKeyInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(Auth);
  const apiKey = authService.getApiKey();

  if (!apiKey) {
    return next(req);
  }

  const authReq = req.clone({
    setHeaders: {
      'X-API-KEY': apiKey,
    },
  });

  return next(authReq);
};
