import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent } from '@delon/form';
import { TreeMenuService } from 'services/treeMenuService';
import { UserService } from 'services/userService';

@Component({
  selector: 'app-sys-setting-menu-add',
  templateUrl: './add.component.html',
  providers: [UserService]
})
export class UserAddComponent implements OnInit {
  record: any = {};

  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      account: { type: 'string', title: 'Account', minLength: 4, maxLength: 16, ui: { placeholder: '请输入账号，初始密码与账号相同' } },
      name: { type: 'string', title: 'Name', maxLength: 16, ui: { placeholder: '实际姓名' } },
      gender: {
        type: 'string', title: 'Gender',
        enum: ['未知', '男', '女'],
        ui: {
          widget: 'radio',
        },
        default: '未知'
      },
      birthday: {
        type: 'string', title: 'Birthday', ui: {
          widget: 'date'
        }
      },
      phone: { type: 'string', title: 'Phone', format: 'mobile', maxLength: 16 },
      adress: { type: 'string', title: 'Adress', maxLength: 128 }
    },
    required: ['account'],
  };
  ui: SFUISchema = {
    '*': {
      spanLabelFixed: 100,
      grid: { span: 12 },
    },
    // $text: { autofocus: true }
  };

  constructor(
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private userService: UserService
  ) {
  }

  ngOnInit(): void { }

  save(value: any) {
    if (value.id > 0) {
      this.userService.update(value).subscribe(f => {
        if (f.code === 0) {
          this.msgSrv.error(f.msg);
        } else {
          this.msgSrv.success(f.msg);
          this.modalRef.close(true);
        }
      });
    } else {
      this.userService.add(value).subscribe(f => {
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
