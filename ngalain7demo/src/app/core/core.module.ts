import { NgModule, Optional, SkipSelf } from '@angular/core';
import { throwIfAlreadyLoaded } from './module-import-guard';

import { I18NService } from './i18n/i18n.service';
import { TreeMenuService } from 'services/treeMenuService';
import { ObjectAclService } from 'services/objectAclService';

@NgModule({
  providers: [
    I18NService,
    TreeMenuService,
    ObjectAclService
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
