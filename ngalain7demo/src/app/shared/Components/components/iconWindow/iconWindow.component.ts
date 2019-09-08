import { Component, OnInit, ViewEncapsulation, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
import { _HttpClient } from '@delon/theme';

@Component({
  selector: 'app-icon-list-window',
  templateUrl: './iconwindow.component.html',
  styles: [
    `
    .choosed{
      /* border:1px blue dotted;*/
      -moz-box-shadow: 5px 5px 2px #91d5ff; /* 老的 Firefox */
      box-shadow: 5px 5px 2px #91d5ff;
    }

    nz-card {
      cursor:pointer
      }

    nz-card:hover{
      background-color:aquamarine
      }
      nz-card:hover i{
        transform: scale(1.3);
				transition: all 0.5s ease 0s;
				-webkit-transform: scale(1.3);
				-webkit-transform: all 0.5s ease 0s;
        }

    nz-card.choosed:hover{
          background-color:transparent
        }
    nz-card.choosed:hover i{
          transform: scale(1);
          transition: all 0.5s ease 0s;
          -webkit-transform: scale(1);
          -webkit-transform: all 0.5s ease 0s;
          }
    `,
  ],
  // encapsulation: ViewEncapsulation.Emulated,
  // changeDetection: ChangeDetectionStrategy.OnPush
})
// https://github.com/angular/angular-cli/issues/10732 case sensitive 更改目录和文件名为小写
export class IconWindowComponent implements OnInit {
  list: any[] = [null];
  value: string;

  constructor(
    private modalRef: NzModalRef,
    private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    // this.http.get('/api/list', { count: 8 }).subscribe((res: any) => {
    //   this.list = this.list.concat(res);
    //   this.loading = false;
    //   this.cdr.detectChanges();
    // });
  }

  choose(iconName: string) {
    this.modalRef.close(iconName);
  }

}
