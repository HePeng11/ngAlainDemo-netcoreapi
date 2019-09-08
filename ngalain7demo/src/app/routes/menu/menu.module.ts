import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { MenuRoutingModule } from './menu-routing.module';
import { MenuListComponent } from './list/list.component';

const COMPONENTS = [
  MenuListComponent];
const COMPONENTS_NOROUNT = [
  ];

@NgModule({
  imports: [
    SharedModule,
    MenuRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class MenuModule { }
