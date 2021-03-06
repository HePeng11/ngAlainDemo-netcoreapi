import { Injectable, Inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { ACLService, DelonACLConfig } from '@delon/acl';
import { MenuService } from '@delon/theme';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';

@Injectable()
export class AuthGuard implements CanActivateChild {
  constructor(private srv: ACLService, private router: Router, private options: DelonACLConfig, private menuService: MenuService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService
  ) { }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.tokenService.get().token) {
      this.router.navigateByUrl('/passport/login');
      return false;
    }
    const path = this.menuService.getPathByUrl(state.url);
    const ignorePaths = ['/exception/', '/dashboard'];
    if (path.length === 0 && ignorePaths.every((f) => {
      if (state.url.indexOf(f) !== 0) {
        return true;
      }
      return false;
    })) {
      return false;
    }
    return true;
    // return true;
  }

}
