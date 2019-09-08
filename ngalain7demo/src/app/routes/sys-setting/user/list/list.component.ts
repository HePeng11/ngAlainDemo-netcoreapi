import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFComponent, SFUISchema } from '@delon/form';
import { STComponent, STPage, STChange, STRequestOptions, STData, STColumn } from '@delon/abc';
import { UserService } from 'services/userService';
import { NzMessageService, NzModalService, UploadFile, NzTableComponent } from 'ng-zorro-antd';
import { UserAddComponent } from '../add/add.component';
import { EditHeadImgComponent } from '../editHeadImg/edit.headimg.component';
import { http_null_param, acls } from 'services/consts';
import { EditPasswordComponent } from '../editPassword/edit.password.component';


@Component({
  selector: 'app-sys-user-list',
  templateUrl: './list.component.html',
  providers: [UserService]
})
export class UserListComponent implements OnInit {
  ability_CUD = acls.users.CUD;
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

  ui: SFUISchema = {
    // $gender: {
    //   grid: { xs: 4},
    // }
  };

  // @ViewChild('st') st: NzTableComponent;
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
    this.userService.query(this.pageIndex, this.pageSize, value.key, value.gender)
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

  constructor(private userService: UserService,
    private modalHelper: ModalHelper,
    private msgSrv: NzMessageService,
    private nzModalService: NzModalService) { }

  /**
   * add or edit
   */
  add(rowData: any) {
    this.modalHelper
      .open(UserAddComponent, { record: rowData }, 'lg')
      .subscribe((v: any) => {
        if (v) {
          this.searchData();
        }
      });
  }


  changePassword(rowData: any) {
    this.modalHelper
      .static(EditPasswordComponent, { record: rowData }, 'sm')
      .subscribe((v: any) => {
      });
  }

  delete(rowData: any) {
    this.nzModalService.confirm({
      nzTitle: '确认删除该用户吗?',
      nzContent: '',
      nzOkText: '是',
      nzOkType: 'danger',
      nzCancelText: '否',
      nzOnOk: () => {
        this.userService.delete(rowData.id).subscribe(f => {
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


  changeHeadImg(userId: number) {
    let img: HTMLImageElement;
    const imgs = document.getElementsByClassName('user_' + userId);
    if (imgs.length > 0) {
      // 强制转换
      img = (<HTMLImageElement>imgs[0]);
    } else { return; }
    const record = { userId: userId, headImg: { url: img.src } };
    this.modalHelper
      .open(EditHeadImgComponent, { record }, 'sm',
        {
          nzOnOk: (f: any) => {
            console.log(f);
          },
        }
      )
      .subscribe((v: any) => {
        if (v && v.length > 0) {
          img.src = v;
        }
      });
  }

}
