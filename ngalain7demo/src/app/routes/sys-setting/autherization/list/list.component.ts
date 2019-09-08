import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { NzMessageService, NzModalService, UploadFile } from 'ng-zorro-antd';
import { AclCategoryService } from 'services/aclCategroyService';
import { ObjectAclService } from 'services/objectAclService';
import { acls } from 'services/consts';
import { ACLService } from '@delon/acl';


@Component({
  selector: 'app-sys-autherization-list',
  templateUrl: './list.component.html',
  providers: [AclCategoryService, ObjectAclService]
})
export class AutherizationListComponent implements OnInit {
  ability_CUD = acls.acl_manage.CUD;
  objectInfo = { name: null, type: null, id: null };
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

  getAclCheckBoxsDisable() {
    return !this.objectInfo.id || this.objectInfo.id === 0 || !this.alainAClSer.canAbility(this.ability_CUD);
  }

  /** type:1表示角色 2用户 */
  choosed(rowData: any, type: number): void {
    this.objectInfo = { name: type === 1 ? rowData.name : rowData.account, type: type, id: rowData.id };
    this.checkAll(false);
    this.queryAcls();
  }

  /**查询并设置权限被选中 */
  queryAcls() {
    this.objAclService.query(this.objectInfo.id, this.objectInfo.type).subscribe((r: number[]) => {
      this.listOfData.forEach((aclRowData: any) => {
        aclRowData.acLs.forEach((acl: any) => {
          acl.own = r.some(d => d === acl.id);
        });
      });

    });
  }


  /**获取是否该行所有acl被选中 */
  getTrAllChecked(aclRowData: any): boolean {
    const r = aclRowData.acLs.every((f: any) => {
      return f.own === true;
    });
    return r;
  }

  /**获取是否该行部分acl被选中 */
  getTrExistChecked(aclRowData: any): boolean {
    if (this.getTrAllChecked(aclRowData)) {
      return false;
    }
    const r = aclRowData.acLs.some((f: any) => {
      return f.own === true;
    });
    return r;
  }

  /**获取是否每一行都被全部选中 */
  getAllTrChecked(aclRowDatas: any[]) {
    return aclRowDatas.every((aclRowData: any) => this.getTrAllChecked(aclRowData));
  }

  /**获取是否存在一行被全部选中 */
  getExistTrChecked(aclRowDatas: any[]) {
    if (this.getAllTrChecked(aclRowDatas)) {
      return false;
    }
    return aclRowDatas.some((aclRowData: any) => this.getTrAllChecked(aclRowData));
  }

  aclChange(v: any) {
    this.saveObjectAcls();
  }

  /**设置该行全部选中 */
  setTrCheckAll(aclRowData: any, v: any, save: boolean = false) {
    aclRowData.acLs.forEach((f: any) => {
      f.own = v;
    });
    if (save) this.saveObjectAcls();
  }

  /**设置每行全部选中 */
  checkAll(v: any, save: boolean = false) {
    this.listOfData.forEach(aclRowData => {
      this.setTrCheckAll(aclRowData, v);
    });
    if (save) this.saveObjectAcls();
  }

  saveObjectAcls() {
    setTimeout(() => {
      const ownAclIds = [];
      const notOwnAclIds = [];
      this.listOfData.forEach(aclRowData => {
        aclRowData.acLs.forEach((acl: any) => {
          acl.own ? ownAclIds.push(acl.id) : notOwnAclIds.push(acl.id);
        });
      });
      this.objAclService.update({ name: this.objectInfo.name, type: this.objectInfo.type, id: this.objectInfo.id, ownAclIds, notOwnAclIds })
        .subscribe((r: any) => {
          // nothing
        });
    }, 100);
  }

  ngOnInit(): void {
    this.searchData();
  }

  constructor(private aclCateSer: AclCategoryService,
    private objAclService: ObjectAclService,
    private alainAClSer: ACLService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }
}
