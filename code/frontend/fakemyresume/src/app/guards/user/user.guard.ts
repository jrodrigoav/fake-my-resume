import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { UserService } from '../../services/user/user.service';
import { map } from 'rxjs/operators';

export const userGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  if(userService.currentUser) return true;
  // The user is loading
  return userService.currentUserChanges.pipe(map(u => u != null));
};
