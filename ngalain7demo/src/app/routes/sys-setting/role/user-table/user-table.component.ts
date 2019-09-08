import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable, Input } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { STComponent, STPage, STChange, STRequestOptions, STData, STColumn } from '@delon/abc';
import { NzMessageService, NzModalService, UploadFile } from 'ng-zorro-antd';
import { http_null_param, acls } from 'services/consts';
import { RoleUserService } from 'services/roleUserService';
import { UserSearchComponent } from '../../user/search/search.component';


@Component({
  selector: 'app-sys-role-user-table',
  templateUrl: './user-table.component.html',
  providers: [RoleUserService]
})
export class RoleUserTableComponent implements OnInit {

  _role: any = {};
  get role(): any {
    return this._role;
  }

  @Input('role')
  set role(value: any) {
    if (value && this._role.id !== value.id) {
      this.checkedIds = {};
      this._role = value;
      this.searchData(true);
    }
  }

  ability_CUD = acls.roles.CUD;
  isIndeterminate = false;
  isAllDataChecked = false;
  checkedIds: { [key: string]: boolean } = {};

  searchSchema: SFSchema = {
    properties: {
      key: {
        type: 'string',
        title: '',
        ui: { placeholder: '输入关键字查询' }
      },
    }
  };

  ui: SFUISchema = {
    $key: {
      grid: { xs: 4, sm: 6, md: 6 },
    }
  };

  @ViewChild('st') st: STComponent;
  @ViewChild('sf') sf: SFComponent;

  pageIndex = 1;
  pageSize = 10;
  pageNo = 0;
  total = 1;
  listOfData = [];
  loading = false;
  sortValue: string | null = null;
  sortKey: string | null = null;
  searchGenderList: string[] = [];

  sort(sort: { key: string; value: string }): void {
    this.sortKey = sort.key;
    this.sortValue = sort.value;
    this.searchData();
  }


  searchData(resetPi: boolean = false): void {
    if (!this.role) return;
    if (resetPi) { this.pageIndex = 1; }
    this.loading = true;
    const value = this.sf.value === undefined ? {} : this.sf.value;
    this.roleUserService.query(this.pageIndex, this.pageSize, this.role.id, value.key)
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
  }

  constructor(private roleUserService: RoleUserService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }

  bindUser() {
    this.modalHelper
      .open(UserSearchComponent, { roleId: this.role.id }, 'lg', { nzStyle: { top: '20px' } })
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }

  delete(roleUserIds: number[]) {
    let msg = '确认从[' + this.role.name + ']角色中移除该用户吗?';
    if (roleUserIds.length > 1) {
      msg = '确认从[' + this.role.name + ']角色中移除选中的用户吗?';
    }
    this.nzModalService.confirm({
      nzTitle: msg,
      nzContent: null,
      nzOkText: '是',
      nzOkType: 'danger',
      nzCancelText: '否',
      nzOnOk: () => {
        this.roleUserService.delete(roleUserIds).subscribe(f => {
          if (f.code === 0) {
            this.msgSrv.error(f.msg);
          } else {
            this.msgSrv.success(f.msg);
            this.searchData();
            this.checkedIds = {};
            this.refreshThStatus();
          }
        });
      }
    });
  }

  checkAll(v: any) {
    this.listOfData.forEach(item => (this.checkedIds[item.roleUserId] = v));
    this.refreshThStatus();
  }

  refreshThStatus() {
    this.isAllDataChecked = this.listOfData.length > 0 && this.listOfData.every(item => this.checkedIds[item.roleUserId]);
    this.isIndeterminate = !this.isAllDataChecked && this.listOfData.some((item, i, a) => this.checkedIds[item.roleUserId]);
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

  canBatchDelete() {
    const ids = this.getCheckIds();
    return ids.length > 0;
  }

  batchDelete() {
    const ids = this.getCheckIds();
    this.delete(ids);
  }

}
