import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';
import { AclCategoryService } from 'services/aclCategroyService';
import { AclService } from 'services/aclService';
import { ACLCategoryAddComponent } from './acl-category-add/acl-category-add.component';
import { ACLAddComponent } from './acl-add/acl-add.component';
import { acls } from 'services/consts';
import { ACLService } from '@delon/acl';


@Component({
  selector: 'app-sys-acl-list',
  templateUrl: './acl-list.component.html',
  styles: [
    `
      .editable-tag {
        background: rgb(255, 255, 255);
        border-style: dashed;
      }
    `
  ],
  providers: [AclCategoryService, AclService]
})
export class ACLListComponent implements OnInit {
  ability_CUD = acls.acls.CUD;
  searchSchema: SFSchema = {
    properties: {
      key: {
        type: 'string',
        title: '',
        ui: { placeholder: '输入关键字查询' }
      }
    },
  };

  ui: SFUISchema = {
  };

  @ViewChild('sf') sf: SFComponent;

  pageIndex = 1;
  pageSize = 10;
  pageNo = 0;
  totol = 1;
  listOfData = [];
  loading = true;
  sortValue: string | null = null;
  sortKey: string | null = null;
  searchGenderList: string[] = [];

  ngOnInit(): void {
    this.searchData();
  }

  searchData(resetPi: boolean = false): void {
    if (resetPi) { this.pageIndex = 1; }
    this.loading = true;
    const value = this.sf.value === undefined ? {} : this.sf.value;
    this.aclCateSer.query(this.pageIndex, this.pageSize, value.key)
      .subscribe((data: any) => {
        this.loading = false;
        this.totol = data.total;
        this.listOfData = data.list;
        this.pageNo = this.pageSize * (this.pageIndex - 1) + 1;
      });
  }

  addCategory(rowData: any) {
    if (!this.alainAClSer.canAbility(this.ability_CUD)) return;
    this.modalHelper
      .open(ACLCategoryAddComponent, { record: rowData }, 'md')
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }

  deleteCategory(rowData: any) {
    this.nzModalService.confirm({
      nzTitle: '确认删除该类别吗?',
      nzContent: '',
      nzOkText: '是',
      nzOkType: 'danger',
      nzCancelText: '否',
      nzOnOk: () => {
        this.aclCateSer.delete(rowData.id).subscribe(f => {
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

  addACL(aclCate: any, acl: any): void {
    if (!this.alainAClSer.canAbility(this.ability_CUD)) return;
    acl.cateId = aclCate.id;
    this.modalHelper
      .open(ACLAddComponent, { aclCate, acl }, 'md')
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }

  deleteACL(tag: any, e: MouseEvent): void {
    e.returnValue = false;
    if (!this.alainAClSer.canAbility(this.ability_CUD)) return;
    this.nzModalService.confirm({
      nzTitle: '确认删除该AC吗?',
      nzContent: '',
      nzOkText: '是',
      nzOkType: 'danger',
      nzCancelText: '否',
      nzOnOk: () => {
        this.aclService.delete(tag.id).subscribe(f => {
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

  constructor(private aclCateSer: AclCategoryService,
    private aclService: AclService,
    private alainAClSer: ACLService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }

}
