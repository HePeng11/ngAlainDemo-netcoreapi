import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { SysSettingRoutingModule } from './sys-setting-routing.module';
import { UserListComponent } from './user/list/list.component';
import { MenuAddComponent } from './menu/add/add.component';
import { MenuTreeListComponent } from './menu/list/list.component';
import { UserAddComponent } from './user/add/add.component';
import { EditHeadImgComponent } from './user/editHeadImg/edit.headimg.component';
import { EditPasswordComponent } from './user/editPassword/edit.password.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AutherizationListComponent } from './autherization/list/list.component';
import { RoleListComponent } from './role/list/list.component';
import { RoleTableComponent } from './role/role-table/role-table.component';
import { RoleUserTableComponent } from './role/user-table/user-table.component';
import { RoleAddComponent } from './role/role-table/add/add.component';
import { UserSearchComponent } from './user/search/search.component';
import { SimpleUserListComponent } from './user/simple-list/simple-list.component';
import { ACLListComponent } from './autherization/acl-list/acl-list.component';
import { ACLCategoryAddComponent } from './autherization/acl-list/acl-category-add/acl-category-add.component';
import { ACLAddComponent } from './autherization/acl-list/acl-add/acl-add.component';

const COMPONENTS = [];
const COMPONENTS_NOROUNT = [
  UserListComponent,
  UserAddComponent,
  MenuAddComponent,
  MenuTreeListComponent,
  EditHeadImgComponent,
  EditPasswordComponent,
  AutherizationListComponent,
  RoleListComponent,
  RoleAddComponent,
  RoleTableComponent,
  RoleUserTableComponent,
  UserSearchComponent,
  SimpleUserListComponent,
  ACLListComponent,
  ACLCategoryAddComponent,
  ACLAddComponent
];

@NgModule({
  imports: [
    SharedModule,
    ReactiveFormsModule,
    SysSettingRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class SysSettingModule { }
