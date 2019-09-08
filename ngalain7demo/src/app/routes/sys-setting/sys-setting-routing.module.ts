import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuTreeListComponent } from './menu/list/list.component';
import { UserListComponent } from './user/list/list.component';
import { AutherizationListComponent } from './autherization/list/list.component';
import { RoleListComponent } from './role/list/list.component';
import { ACLListComponent } from './autherization/acl-list/acl-list.component';

const routes: Routes = [
  { path: 'menu', component: MenuTreeListComponent },
  { path: 'user', component: UserListComponent },
  { path: 'role', component: RoleListComponent },
  { path: 'acl', component: ACLListComponent },
  { path: 'autherization', component: AutherizationListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SysSettingRoutingModule { }
