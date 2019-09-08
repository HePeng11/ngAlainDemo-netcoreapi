import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent } from '@delon/form';
import { RoleService } from 'services/RoleService';

@Component({
  selector: 'app-sys-setting-menu-add',
  templateUrl: './add.component.html',
  providers: [RoleService]
})
export class RoleAddComponent implements OnInit {
  record: any = {};

  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      name: { type: 'string', title: '名称', minLength: 2, maxLength: 16, ui: { placeholder: '请输入角色名称' } },
      description: { type: 'string', title: '描述', maxLength: 128, ui: { placeholder: '角色描述' } }
    },
    required: ['name'],
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
    private roleService: RoleService
  ) {
  }

  ngOnInit(): void { }

  save(value: any) {
    if (value.id > 0) {
      this.roleService.update(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    } else {
      this.roleService.add(value).subscribe(f => {
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
