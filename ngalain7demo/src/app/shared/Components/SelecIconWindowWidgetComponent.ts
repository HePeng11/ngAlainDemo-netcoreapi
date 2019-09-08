import { Component, OnInit, ChangeDetectorRef, Injector } from '@angular/core';
import { ControlWidget, SFItemComponent, SFComponent } from '@delon/form';
import { ICONS_AUTO } from 'style-icons-auto';
import { ModalHelper } from '@delon/theme';
import { IconWindowComponent } from './components/iconwindow/iconwindow.component';
import { ngZorro_icons } from '@shared/icons/ngZorro_icons';

@Component({
  selector: 'app-custom-select-icon',
  template: `<sf-item-wrap [id]="id" [schema]="schema" [ui]="ui" [showError]="showError" [error]="error" [showTitle]="schema.title">
    <nz-input-group [nzPrefix]="prefixTemplate">
      <input type="text" nz-input placeholder="choose icon" [(ngModel)]='value' (click)="openIconWindow()" />
    </nz-input-group>
    <ng-template #prefixTemplate><i nz-icon [type]="(value||'').replace('anticon anticon-', '')"></i></ng-template>
  </sf-item-wrap>`
})

export class SelecIconWindowWidgetComponent extends ControlWidget implements OnInit {

  static readonly KEY = 'SelecIconWindow';

  // 组件所需要的参数，建议使用 `ngOnInit` 获取
  value: string;

  icons = [];

  ngOnInit(): void {
    this.icons = this.icons.concat(ICONS_AUTO);
    this.icons = this.icons.concat(ngZorro_icons);
  }

  openIconWindow() {
    this.modalHelper
      .open(IconWindowComponent, { list: this.icons, value: (this.value || '').replace('anticon anticon-', '') }, 1000,
        {
          nzOnOk: (f: any) => { },
        }
      )
      .subscribe((v: any) => {
        if (v) {
          this.setValue('anticon anticon-' + v);
        }
      });
  }

  constructor(private modalHelper: ModalHelper, cd: ChangeDetectorRef, injector: Injector, sfItemComp?: SFItemComponent,
    sfComp?: SFComponent) {
    super(cd, injector, sfItemComp, sfComp);
  }
}
