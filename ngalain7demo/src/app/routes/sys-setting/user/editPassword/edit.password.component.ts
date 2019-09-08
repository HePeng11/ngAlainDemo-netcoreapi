import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent, TreeSelectWidget, SFUISchemaItem, SFSchemaEnumType } from '@delon/form';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'services/userService';

@Component({
  selector: 'app-sys-setting-user-edit-password',
  templateUrl: './edit.password.component.html',
  providers: [UserService],
  styles: [
    `
      i {
        cursor: pointer;
      }
    `
  ]
})
export class EditPasswordComponent implements OnInit {
  record: any = {};
  passwordVisible = false;
  password: string;
  form: FormGroup;

  constructor(
    fb: FormBuilder,
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private userService: UserService
  ) {
    this.form = fb.group({
      password: [null, [Validators.required, Validators.minLength(6)]],
    });
  }

  ngOnInit(): void {
  }

  save() {
    this.userService.updatePassword(this.record.id, this.form.value.password).subscribe(f => {
      f.code === 0 ? this.msgSrv.error(f.msg) : this.msgSrv.success(f.msg);
      if (f.code === 1) this.modalRef.close(true);
    });
  }

  close() {
    this.modalRef.destroy();
  }
}
