import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Injectable, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { TreeMenuService, TreeNodeInterface } from 'services/treeMenuService';
import { ModalHelper, DrawerHelper } from '@delon/theme';
import { STComponent } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { NzModalService, NzMessageService } from 'ng-zorro-antd';
import { MenuAddComponent } from '../add/add.component';
import { acls } from 'services/consts';

@Component({
  selector: 'app-menu-list',
  templateUrl: './list.component.html',
  providers: [TreeMenuService]
})
export class MenuTreeListComponent implements OnInit {
  ability_CUD = acls.menus.CUD;
  listOfMapData = [];
  mapOfExpandedData: { [key: string]: TreeNodeInterface[] } = {};

  pageIndex = 1;
  pageSize = 10;
  total = 1;
  loading = true;

  //#region  about treetable
  collapse(array: TreeNodeInterface[], data: TreeNodeInterface, $event: boolean): void {
    if ($event === false) {
      if (data.children) {
        data.children.forEach(d => {
          const target = array.find(a => a.key === d.key);
          target.expand = false;
          this.collapse(array, target, false);
        });
      } else {
        return;
      }
    }
  }

  convertTreeToList(root: any, expand: boolean): TreeNodeInterface[] {
    const stack: any[] = [];
    const array: any[] = [];
    const hashMap = {};
    stack.push({ ...root, level: 0, expand: expand });

    while (stack.length !== 0) {
      const node = stack.pop();
      if (!hashMap[node.key]) {
        hashMap[node.key] = true;
        array.push(node);
        // console.log(root.key + ' : ' + node.key);
      }
      if (node.children && node.children.length === 0) {
        node.children = null;
      }
      if (node.children) {
        for (let i = node.children.length - 1; i >= 0; i--) {
          stack.push({ ...node.children[i], level: node.level + 1, expand: false, parent: node });
        }
      }
    }

    return array;
  }

  ngOnInit(): void {
    this.searchData(true);
  }


  searchData(reset: boolean = false): void {
    if (reset) { this.pageIndex = 1; }
    this.loading = true;
    this.treeMenuService
      .queryMenus(this.pageIndex, this.pageSize)
      .subscribe((data: any) => {
        this.loading = false;
        this.total = data.total;
        this.listOfMapData = data.list;
        let i = 0;
        this.listOfMapData.forEach(item => {
          let expand = false;
          if (i < 2) {
            expand = true;
            i += 1;
          }
          this.mapOfExpandedData[item.key] = this.convertTreeToList(item, expand);
        });
      });
  }
  //#endregion

  constructor(private treeMenuService: TreeMenuService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService
  ) { }

  add(rowData: any) {
    this.modalHelper
      .open(MenuAddComponent, { record: rowData }, 'lg',
        {
          nzOnOk: (f: any) => {
            this.searchData();
          },
        }
      )
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }

  delete(rowData: any) {
    this.nzModalService.confirm({
      nzTitle: 'Are you sure delete this menu?',
      nzContent: rowData.children && rowData.children.length > 0 ? '<b style="color: red;">将删除其子菜单</b>' : '',
      nzOkText: 'Yes',
      nzOkType: 'danger',
      nzCancelText: 'No',
      nzOnOk: () => {
        this.treeMenuService.deleteMenu(rowData).subscribe(f => {
          if (f.code === 0) {
            this.msgSrv.error(f.msg);
          }
          this.msgSrv.success(f.msg);
          this.searchData();
        });
      }
    });

  }

}


