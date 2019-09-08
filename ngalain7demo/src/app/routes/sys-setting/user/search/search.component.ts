import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { STComponent, STPage, STChange, STRequestOptions, STData, STColumn } from '@delon/abc';
import { UserService } from 'services/userService';
import { NzMessageService, NzModalService, UploadFile, NzModalRef } from 'ng-zorro-antd';
import { UserAddComponent } from '../add/add.component';
import { http_null_param } from 'services/consts';
import { RoleUserService } from 'services/roleUserService';


@Component({
  selector: 'app-sys-user-search',
  templateUrl: './search.component.html',
  providers: [UserService, RoleUserService]
})

/**弹窗查询某角色未绑定的用户列表 后期需定义一个公用用户列表组件 */
export class UserSearchComponent implements OnInit {
  roleId = 0; // 传入参数
  isIndeterminate = false;
  isAllDataChecked = false;
  checkedIds: { [key: string]: boolean } = {};

  http_kong_param = http_null_param;
  searchSchema: SFSchema = {
    properties: {
      key: {
        type: 'string',
        title: '关键字',
      },
      gender: {
        type: 'string',
        title: '性别',
        default: this.http_kong_param,
        enum: [
          { label: '请选择', value: this.http_kong_param },
          { label: '未知', value: '0' },
          { label: '男', value: '1' },
          { label: '女', value: '2' }
        ],
        ui: {
          widget: 'select',
          beforeValue: this.http_kong_param,
          change: (v: any) => {
            setTimeout(() => {
              this.searchData(true);
            }, 10);
          },
          width: 130
        }
      }
    },
  };

  ui: SFUISchema = {};

  @ViewChild('st') st: STComponent;
  @ViewChild('sf') sf: SFComponent;

  pageIndex = 1;
  pageSize = 10;
  pageNo = 0;
  total = 1;
  listOfData = [];
  loading = true;
  sortValue: string | null = null;
  sortKey: string | null = null;
  searchGenderList: string[] = [];

  searchData(resetPi: boolean = false): void {
    if (resetPi) { this.pageIndex = 1; }
    this.loading = true;
    const value = this.sf.value === undefined ? {} : this.sf.value;
    this.roleUserService.QueryIsNotRole(this.pageIndex, this.pageSize, this.roleId, value.key, value.gender)
      .subscribe((data: any) => {
        this.loading = false;
        this.total = data.total;
        this.listOfData = data.list;
        this.pageNo = this.pageSize * (this.pageIndex - 1) + 1;
        this.refreshThStatus();
      });
  }

  restSfSt(): void {
    this.sf.reset();
    this.searchData();
  }

  ngOnInit(): void {
    this.searchData();
  }

  constructor(private userService: UserService,
    private roleUserService: RoleUserService,
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }


  checkAll(v: any) {
    this.listOfData.forEach(item => (this.checkedIds[item.id] = v));
    this.refreshThStatus();
  }

  refreshThStatus() {
    this.isAllDataChecked = this.listOfData.length > 0 && this.listOfData.every(item => this.checkedIds[item.id]);
    this.isIndeterminate = !this.isAllDataChecked && this.listOfData.some((item, i, a) => this.checkedIds[item.id]);
  }

  close() {
    this.modalRef.destroy();
  }

  private getCheckIds(): number[] {
    const ids = [];
    for (const key in this.checkedIds) {
      if (this.checkedIds.hasOwnProperty(key)) {
        const e = this.checkedIds[key];
        if (e) {
          ids.push(key);
        }
      }
    }
    return ids;
  }

  canSave() {
    return this.getCheckIds().length > 0;
  }

  save() {
    const roles = this.getCheckIds().map(item => {
      return { roleId: this.roleId, userId: item };
    });
    this.roleUserService.add(roles).subscribe(f => {
      if (f.code === 0) {
        this.msgSrv.error(f.msg);
      } else {
        this.msgSrv.success(f.msg);
        this.modalRef.close(true);
      }
    });
  }

}
