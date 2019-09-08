import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent } from '@delon/form';
import { AclService } from 'services/aclService';

@Component({
  selector: 'app-sys-setting-acl-add',
  templateUrl: './acl-add.component.html',
  providers: [AclService]
})
export class ACLAddComponent implements OnInit {
  aclCate: any = {};
  acl: any = {};

  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      name: { type: 'string', title: '名称', minLength: 2, maxLength: 16, ui: { placeholder: 'ACL名称' } },
      code: { type: 'string', title: '编码', minLength: 2, maxLength: 16, ui: { placeholder: 'ACL编码' } },
      description: { type: 'string', title: '描述', maxLength: 32, ui: { placeholder: '可填写相关按钮或功能名称' } }
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
    private aclService: AclService
  ) {
  }

  ngOnInit(): void { }

  save(value: any) {
    if (value.id > 0) {
      this.aclService.update(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    } else {
      this.aclService.add(value).subscribe(f => {
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
