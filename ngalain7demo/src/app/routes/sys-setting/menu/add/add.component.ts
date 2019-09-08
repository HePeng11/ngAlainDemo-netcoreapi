import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent, TreeSelectWidget, SFUISchemaItem, SFSchemaEnumType } from '@delon/form';
import { SelectWithIconWidgetComponent } from '@shared/Components/SelectWithIconWidgetComponent';
import { of, Operator, Subscriber, Observable } from 'rxjs';
import { delay } from 'rxjs/operators';
import { TreeSelectWidgetComponent } from '@shared/Components/TreeSelectWidgetComponent';
import { SelecIconWindowWidgetComponent } from '@shared/Components/SelecIconWindowWidgetComponent';
import { TreeMenuService } from 'services/treeMenuService';

@Component({
  selector: 'app-sys-setting-menu-add',
  templateUrl: './add.component.html',
  providers: [TreeMenuService]
})
export class MenuAddComponent implements OnInit {
  record: any = {};

  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      parentId: {
        type: 'integer',
        title: 'parentId',
        ui: {
          widget: 'tree-select',
          asyncData: () => Observable.create((observer: any) => {
            return this.treeMenuService.getMenusExcept(this.record.id || 0).subscribe((f: any) => {
              f.forEach((menu: any) => {
                this.setMenuSelectTreeNode(menu);
              });
              observer.next(f);
            });
          })
        }
      },
      text: { type: 'string', title: 'Text', maxLength: 16 },
      link: { type: 'string', title: 'Link', maxLength: 64 },
      isExternalLink: { type: 'boolean', title: 'IsExternalLink' },
      icon: {
        type: 'string', title: 'Icon', maxLength: 64,
        ui: { widget: SelecIconWindowWidgetComponent.KEY }
      },
      disabled: { type: 'boolean', title: 'Disabled' },
      showOrder: { type: 'integer', title: 'ShowOrder' },
      aclCode: { type: 'string', title: 'AclCode', maxLength: 16 },
    },
    required: ['text'],
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 150,
      grid: { span: 12 },
    },
    $text: { autofocus: true }
  };

  constructor(
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private treeMenuService: TreeMenuService
  ) {
  }

  ngOnInit(): void {
    // 显示默认值 不然页面tree-select有bug
    const i = setInterval(() => {
      if (this.sf) {
        this.sf.validator();
        clearInterval(i);
      }
    }, 300);
  }

  setMenuSelectTreeNode(menu: any): void {
    menu.title = menu.text;
    menu.key = menu.key;
    if (menu.children && menu.children.length > 0) {
      menu.children.forEach((m: any) => {
        this.setMenuSelectTreeNode(m);
      });
    } else {
      menu.isLeaf = true;
    }
  }

  save(value: any) {
    // 指定了subscribe才会请求 wo***nima
    if (value.id > 0) {
      this.treeMenuService.updateMenu(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    } else {
      this.treeMenuService.addMenu(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    }

  }

  close() {
    this.modalRef.destroy();
  }
}
