import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable, EventEmitter, Output } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { STComponent, STPage, STChange, STRequestOptions, STData, STColumn } from '@delon/abc';
import { UserService } from 'services/userService';
import { NzMessageService, NzModalService, UploadFile, NzTableComponent } from 'ng-zorro-antd';
import { UserAddComponent } from '../add/add.component';
import { EditHeadImgComponent } from '../editHeadImg/edit.headimg.component';
import { http_null_param } from 'services/consts';
import { EditPasswordComponent } from '../editPassword/edit.password.component';


@Component({
  selector: 'app-sys-user-simple-list',
  templateUrl: './simple-list.component.html',
  styles: [
    `
    .choosed{
      background: #bae7ff;
    }
    `,
  ],
  providers: [UserService]
})
/**简易用户列表，暂时仅用于查看 */
export class SimpleUserListComponent implements OnInit {
  @Output() clickRow: EventEmitter<any> = new EventEmitter<any>();
  choosedId = 0;
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

  sort(sort: { key: string; value: string }): void {
    this.sortKey = sort.key;
    this.sortValue = sort.value;
    this.searchData();
  }

  searchData(resetPi: boolean = false): void {
    if (resetPi) { this.pageIndex = 1; }
    this.loading = true;
    const value = this.sf.value === undefined ? {} : this.sf.value;
    this.userService.query(this.pageIndex, this.pageSize, value.key, null)
      .subscribe((data: any) => {
        this.loading = false;
        this.totol = data.total;
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

  clickTr(rowData: any, e: any) {
    if (rowData) {
      this.choosedId = rowData.id;
      this.clickRow.emit(rowData);
    }
  }

  constructor(private userService: UserService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }

}
