import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { ACLService, DelonACLConfig } from '@delon/acl';
import { MenuService } from '@delon/theme';

@Injectable()
export class AuthGuard1 implements CanActivate {

  constructor(private srv: ACLService, private router: Router, private options: DelonACLConfig, private menuService: MenuService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const path = this.menuService.getPathByUrl(state.url);
    if (path.length === 0) {
      return false;
    }
    return true;
  }
}
