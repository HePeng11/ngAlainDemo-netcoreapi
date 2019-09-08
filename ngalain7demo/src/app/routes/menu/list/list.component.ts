import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STPage, STChange, STData, STRequestOptions, STColumnButton } from '@delon/abc';
import { SFSchema, SFComponent } from '@delon/form';
import { debug } from 'util';
import { MenuAddComponent } from '../../sys-setting/menu/add/add.component';
import { RtlScrollAxisType } from '@angular/cdk/platform';
import { text } from '@angular/core/src/render3';
import { toarry } from '@shared';

@Component({
  selector: 'app-menu-list',
  templateUrl: './list.component.html',
})
export class MenuListComponent implements OnInit {
  isVisible = false;
  url = `/user`;
  searchSchema: SFSchema = {
    properties: {
      no: {
        type: 'string',
        title: '编号',
      },
      description: {
        type: 'string',
        title: '描述',
      },
    },
  };
  status = [
    { index: 0, text: '关闭', value: false, type: 'default', checked: false },
    {
      index: 1,
      text: '运行中',
      value: false,
      type: 'processing',
      checked: false,
    },
    { index: 2, text: '已上线', value: false, type: 'success', checked: false },
    { index: 3, text: '异常', value: false, type: 'error', checked: false },
  ];
  @ViewChild('st') st: STComponent;
  @ViewChild('sf') sf: SFComponent;
  columns: STColumn[] = [
    {
      title: '',
      index: 'sdfasds',
      type: 'checkbox',
      selections: [
        {
          text: 'id<5',
          select: data => {
            data.forEach(item => (item.checked = item.id < 5));
          },
        },
        {
          text: 'id>=5',
          select: data => {
            data.forEach(item => (item.checked = item.id > 5));
          },
        },
      ],
    },
    { title: '编号', index: 'no', className: 'text-danger' },
    {
      title: '调用次数',
      index: 'callNo',
      // sorter: (a: any, b: any) => a.callNo - b.callNo, //前端分页使用sorter 后端分页使用sort
      sort: true
    },
    { title: '头像', type: 'img', index: 'avatar' },
    { title: '时间', type: 'date', index: 'updatedAt', sort: true },
    { title: '描述', index: 'description' },
    {
      title: '操作',
      buttons: [
        {
          text: '查看', click: (item: any) => {
            alert(JSON.stringify(item));
          }
        },
        {
          text: '编辑', click: (item: any) => {
            this.edit(item);
          }
        }
      ],
    },
  ];

  stPage: STPage = {
    pageSizes: [10, 20, 30],
    showQuickJumper: true,
    showSize: true,
  };

  params = { a: 1, b: 2 };
  selectedRows = [];
  totalCallNo = 0;

  constructor(
    private http: _HttpClient,
    private modal: ModalHelper,
    private cdr: ChangeDetectorRef,
  ) { }

  ngOnInit() { }

  add() {
    this.modal
      .open(
        MenuAddComponent,
        { record: { id: 0 } },
        'lg',
        {
          nzOnOk: (f: any) => {
            this.st.reload();
          },
          // nzOnCancel: f => this.st.reload()
        }
      )
      .subscribe((v: any) => {
        if (v) {
          this.st.reload();
        }
      });
    // this.isVisible = true;
  }

  edit(item: any) {
    this.modal
      .open(
        MenuAddComponent,
        { record: { id: item.id } },
        'lg',
        {
          nzOnOk: (f: any) => {
            this.st.reload();
          },
          // nzOnCancel: f => this.st.reload()
        }
      )
      .subscribe((v: any) => {
        if (v) {
          this.st.reload();
        }
      });
  }

  // formSumbmit(e: any) {
  //   this.st.reset(e);
  // }

  // formReset() {
  //   this.sf.reset();
  //   this.st.reset({});
  // }

  change(e: STChange) {
    switch (e.type) {
      case 'checkbox':
        this.selectedRows = e.checkbox;
        for (let i = 0; i < this.st._data.length; i++) {
          const element = this.st._data[i];
          console.log(element.no, element.sdfasds);
        }
        this.totalCallNo = this.selectedRows.reduce((total, cv) => {
          return total + cv.callNo;
        }, 0);
        this.cdr.detectChanges();
        break;
      default:
        break;
    }
  }

  // 更改请求参数
  reqProcess(requestOptions: STRequestOptions) {
    const sort = toarry(requestOptions.params).filter(f => f.value === 'descend' || f.value === 'ascend')[0];
    if (sort) {
      // delete requestOptions.params[sort.key];
      // requestOptions.params['sort'] = { ddd: 'dfa' };
    }
    return requestOptions;
  }

  // 获取数据后的回调函数 disabled checked expand 是行数据的三个固定值
  dataChange(data: STData[]) {
    this.selectedRows = [];
    this.totalCallNo = 0;
    // this.cdr.detectChanges();
    return data.map((i: STData, index: number) => {
      i.disabled = index === 0;
      return i;
    });
  }

  setVisible(v: boolean) {
    this.isVisible = v;
  }
}
