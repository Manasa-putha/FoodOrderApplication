import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToasterService } from '../services/toaster.service';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const toasterService = inject(ToasterService);

  if (auth.isLoggedIn()) {
    return true;
  } else {
    toasterService.showError('ERROR', 'Please Login First!');
    router.navigate(['login']);
    return false;
  }
};
