import { Component, OnInit, ViewChild } from '@angular/core';
import { NzModalRef, NzMessageService, NzDrawerRef, NzTreeNode } from 'ng-zorro-antd';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SFSchema, SFUISchema, SFComponent, TreeSelectWidget, SFUISchemaItem, SFSchemaEnumType } from '@delon/form';
import { UploadUserHeadImgWidgetComponent } from '@shared/Components/UploadUserHeadImgWidgetComponent';
import { FileService } from 'services/fileService';

@Component({
  selector: 'app-sys-setting-user-add-edit-headimg',
  templateUrl: './edit.headimg.component.html',
  providers: [FileService]
})
export class EditHeadImgComponent implements OnInit {
  record: any = {};
  userId: number;
  @ViewChild('sf') sf: SFComponent;
  schema: SFSchema = {
    properties: {
      headImg: {
        type: 'string', title: '', ui: {
          widget: UploadUserHeadImgWidgetComponent.KEY
        }
      }
    }
  };
  ui: SFUISchema = {
    '*': {
      spanControl: 24,
      grid: { span: 24 },
    }
  };

  constructor(
    private modalRef: NzModalRef,
    private msgSrv: NzMessageService,
    public http: _HttpClient,
    private fileService: FileService
  ) {
  }

  ngOnInit(): void {
  }

  save(value: any) {
    if (value.headImg.fileId > 0) {
      this.fileService.saveRelation(value.headImg.fileId, value.userId).subscribe((f) => {
        f.code === 0 ? this.msgSrv.error(f.msg) : this.msgSrv.success(f.msg);
        if (f.code === 1) this.modalRef.close(value.headImg.url);
      });
    }
  }

  close() {
    this.modalRef.destroy();
  }
}
