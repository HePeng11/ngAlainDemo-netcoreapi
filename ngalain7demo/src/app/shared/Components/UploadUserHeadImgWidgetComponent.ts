import { Component, OnInit, ChangeDetectorRef, Injector } from '@angular/core';
import { ControlWidget, SFItemComponent, SFComponent } from '@delon/form';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { Observable, Observer } from 'rxjs';
import { webApiUrls } from 'services/webApiUrls';

@Component({
  selector: 'app-custom-upload-headimg-icon',
  styles: [
    `
      .avatar {
        width: 200px;
        height: 200px;
      }
      .upload-icon {
        font-size: 32px;
        color: #999;
      }
      .ant-upload-text {
        margin-top: 8px;
        color: #666;
      }
    `
  ],
  template: `<sf-item-wrap [id]="id" [schema]="schema" [ui]="ui" [showError]="showError" [error]="error" [showTitle]="schema.title">
  <nz-upload class="avatar-uploader"
  nzAction="${webApiUrls.files.upload}?uploadType=1"
  nzName="avatar"
  nzListType="picture-card"
  [nzShowUploadList]="false"
  [nzBeforeUpload]="beforeUpload"
  (nzChange)="handleChange($event)">
    <ng-container *ngIf="!value">
      <i class="upload-icon" nz-icon [nzType]="loading ? 'loading' : 'plus'"></i>
      <div class="ant-upload-text">Upload</div>
    </ng-container>
    <img *ngIf="value" [src]="value.url" class="avatar" />
  </nz-upload>
  </sf-item-wrap>`
})

export class UploadUserHeadImgWidgetComponent extends ControlWidget implements OnInit {

  static readonly KEY = 'UploadUserHeadImg';

  loading = false;

  ngOnInit(): void {
  }

  beforeUpload = (file: File) => {
    return new Observable((observer: Observer<boolean>) => {
      const isJPG = file.type === 'image/jpeg';
      // if (!isJPG) {
      //   this.msg.error('You can only upload JPG file!');
      //   observer.complete();
      //   return;
      // }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.msg.error('Image must smaller than 2MB!');
        observer.complete();
        return;
      }
      // check height
      // this.checkImageDimension(file).then(dimensionRes => {
      //   if (!dimensionRes) {
      //     this.msg.error('Image only 300x300 above');
      //     observer.complete();
      //     return;
      //   }

      //   observer.next(isJPG && isLt2M && dimensionRes);
      //   observer.complete();
      // });
      observer.next(isLt2M);
      observer.complete();
    });
  }

  private getBase64(img: File, callback: (img: string) => void): void {
    const reader = new FileReader();
    // tslint:disable-next-line: no-non-null-assertion
    // reader.addEventListener('load', () => callback(reader.result!.toString()));
    reader.readAsDataURL(img);
  }

  private checkImageDimension(file: File): Promise<boolean> {
    return new Promise(resolve => {
      const img = new Image(); // create image
      img.src = window.URL.createObjectURL(file);
      img.onload = () => {
        const width = img.naturalWidth;
        const height = img.naturalHeight;
        window.URL.revokeObjectURL(img.src);
        resolve(width === height && width >= 300);
      };
    });
  }

  handleChange(info: { file: UploadFile }): void {
    switch (info.file.status) {
      case 'uploading':
        this.loading = true;
        break;
      case 'done':
        // Get this url from response in real world.
        // tslint:disable-next-line: no-non-null-assertion
        // this.getBase64(info.file!.originFileObj!, (img: string) => {
        //   this.loading = false;
        //   this.avatarUrl = img;
        // });
        this.loading = false;
        this.setValue({ fileId: info.file.response.fileId, url: info.file.response.url });
        break;
      case 'error':
        this.msg.error('Network error');
        this.loading = false;
        break;
    }
  }

  constructor(private msg: NzMessageService, cd: ChangeDetectorRef, injector: Injector, sfItemComp?: SFItemComponent,
    sfComp?: SFComponent) {
    super(cd, injector, sfItemComp, sfComp);
  }
}
