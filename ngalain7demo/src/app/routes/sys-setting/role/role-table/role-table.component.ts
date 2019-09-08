import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable, Output, EventEmitter, Input } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { STComponent, STPage, STChange, STRequestOptions, STData, STColumn } from '@delon/abc';
import { RoleService } from 'services/RoleService';
import { NzMessageService, NzModalService, UploadFile } from 'ng-zorro-antd';
import { RoleAddComponent } from './add/add.component';
import { RoleUserTableComponent } from '../user-table/user-table.component';
import { acls } from 'services/consts';
import { ACLService } from '@delon/acl';


@Component({
  selector: 'app-sys-role-table',
  templateUrl: './role-table.component.html',
  providers: [RoleService],
  styles: [
    `
    .choosed{
      background: #bae7ff;
    }
    `,
  ],

})
export class RoleTableComponent implements OnInit {
  @Output() clickRow: EventEmitter<any> = new EventEmitter<any>();
  @Input() viewMode = false;

  ability_CUD = acls.roles.CUD;
  choosedRoleId = 0;
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
      grid: { xs: 4, sm: 4, md: 4, lg: 4 },
    }
  };

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

  sort(sort: { key: string; value: string }): void {
    this.sortKey = sort.key;
    this.sortValue = sort.value;
    this.searchData();
  }


  searchData(resetPi: boolean = false): void {
    if (resetPi) { this.pageIndex = 1; }
    this.loading = true;
    const value = this.sf.value === undefined ? {} : this.sf.value;
    this.roleService.query(this.pageIndex, this.pageSize, value.key)
      .subscribe((data: any) => {
        this.loading = false;
        this.total = data.total;
        this.listOfData = data.list;
        this.pageNo = this.pageSize * (this.pageIndex - 1) + 1;
      });
  }

  restSfSt(): void {
    this.sf.reset();
    this.searchData();
  }

  ngOnInit(): void {
    this.searchData();
  }

  constructor(private roleService: RoleService,
    private alainAClSer: ACLService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }

  /**
   * add or edit
   */
  add(rowData: any) {
    this.modalHelper
      .open(RoleAddComponent, { record: rowData }, 'md')
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }

  delete(rowData: any) {
    this.nzModalService.confirm({
      nzTitle: '确认删除该角色吗?',
      nzContent: '',
      nzOkText: '是',
      nzOkType: 'danger',
      nzCancelText: '否',
      nzOnOk: () => {
        this.roleService.delete(rowData.id).subscribe(f => {
          if (f.code === 0) {
            this.msgSrv.error(f.msg);
          } else {
            this.msgSrv.success(f.msg);
            this.searchData();
          }
        });
      }
    });
  }

  showRoleUserData(rowData: any, e: any) {
    if (rowData) {
      this.choosedRoleId = rowData.id;
      this.clickRow.emit(rowData);
    }
  }

  showRoleUsersWindow(rowData: any) {
    this.modalHelper
      .open(RoleUserTableComponent, { role: rowData }, 'lg', { nzStyle: { top: '20px' } })
      .subscribe((v: any) => {

      });
  }

}
