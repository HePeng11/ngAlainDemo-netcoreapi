import { Component, OnInit, ChangeDetectorRef, Injector } from '@angular/core';
import { ControlWidget, SFItemComponent, SFComponent } from '@delon/form';
import { ICONS_AUTO } from 'style-icons-auto';

@Component({
  selector: 'app-custom-select-icon',
  template: `<sf-item-wrap [id]="id" [schema]="schema" [ui]="ui" [showError]="showError" [error]="error" [showTitle]="schema.title">
  <i nz-icon [type]="selectedIcon"></i>
    <nz-select class="pl-sm" style="width:90%" nzShowSearch nzAllowClear nzPlaceHolder="Select"
    [(ngModel)]="selectedIcon" (ngModelChange)="change($event)">
    <ng-container *ngFor="let icon of icons">
      <nz-option nzCustomContent nzLabel="anticon anticon-{{icon.name}}" nzValue="anticon anticon-{{icon.name}}" >
      <i nz-icon [type]="icon.name"></i> anticon anticon-{{icon.name}}
      </nz-option>
    </ng-container>
    </nz-select>
  </sf-item-wrap>`
})

export class SelectWithIconWidgetComponent extends ControlWidget implements OnInit {

  /* 用于注册小部件 KEY 值 */
  static readonly KEY = 'SelectWithIcon';

  // 组件所需要的参数，建议使用 `ngOnInit` 获取
  selectedIcon: string;
  icons = ICONS_AUTO;
  ngOnInit(): void {
    this.selectedIcon = this.ui.selectedIcon;
  }

  // reset 可以更好的解决表单重置过程中所需要的新数据问题
  reset(value: string) {

  }

  change(value: string) {
    if (this.ui.change) this.ui.change(value);
    this.setValue(value);
  }

  // constructor(cd: ChangeDetectorRef, injector: Injector, sfItemComp?: SFItemComponent, sfComp?: SFComponent,
  //   private domSanitizer: DomSanitizer
  // ) {
  //   super(cd, injector, sfItemComp, sfComp);
  // }
}
