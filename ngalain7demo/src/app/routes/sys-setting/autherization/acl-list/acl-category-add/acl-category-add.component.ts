import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent } from '@delon/form';
import { AclCategoryService } from 'services/aclCategroyService';

@Component({
  selector: 'app-sys-setting-acl-category-add',
  templateUrl: './acl-category-add.component.html',
  providers: [AclCategoryService]
})
export class ACLCategoryAddComponent implements OnInit {
  record: any = {};

  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      name: { type: 'string', title: '名称', minLength: 2, maxLength: 16, ui: { placeholder: '类别名称' } },
      code: { type: 'string', title: '编码', minLength: 2, maxLength: 16, ui: { placeholder: '类别编码' } }
    },
    required: ['name', 'code'],
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 24 },
    },
    // $text: { autofocus: true }
  };

  constructor(
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private aclCateSer: AclCategoryService
  ) {
  }

  ngOnInit(): void { }

  save(value: any) {
    if (value.id > 0) {
      this.aclCateSer.update(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    } else {
      this.aclCateSer.add(value).subscribe(f => {
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
