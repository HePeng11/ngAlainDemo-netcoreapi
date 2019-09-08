import { Injectable, Injector, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MenuService, SettingsService, TitleService, ALAIN_I18N_TOKEN } from '@delon/theme';
import { DA_SERVICE_TOKEN, ITokenService, TokenService } from '@delon/auth';
import { ACLService } from '@delon/acl';
import { TranslateService } from '@ngx-translate/core';
import { I18NService } from '../i18n/i18n.service';

import { NzIconService } from 'ng-zorro-antd';
import { ICONS_AUTO } from '../../../style-icons-auto';
import { ICONS } from '../../../style-icons';
import { webApiUrls, appSettings } from 'services/webApiUrls';
import { TreeMenuService } from 'services/treeMenuService';
import { ngZorro_icons } from '@shared/icons/ngZorro_icons';
import { ObjectAclService } from 'services/objectAclService';

/**
 * 用于应用启动时
 * 一般用来获取应用所需要的基础数据等
 */
@Injectable()
export class StartupService {
  constructor(
    iconSrv: NzIconService,
    private menuService: MenuService,
    private translate: TranslateService,
    @Inject(ALAIN_I18N_TOKEN) private i18n: I18NService,
    private settingService: SettingsService,
    private aclService: ACLService,
    private titleService: TitleService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private httpClient: HttpClient,
    private injector: Injector,
    private treeMenuService: TreeMenuService,
    private objAclService: ObjectAclService
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS, ...ngZorro_icons);
  }

  private setApp(resolve: any, reject: any) {
    this.httpClient.get(`${location.protocol + '//' + location.host}/assets/tmp/i18n/${this.i18n.defaultLang}.json`)
      .subscribe((langData) => {
        // setting language data
        this.translate.setTranslation(this.i18n.defaultLang, langData);
        this.translate.setDefaultLang(this.i18n.defaultLang);
        // 应用信息：包括站点名、描述、年份
        this.settingService.setApp(appSettings.appInfo);
        // 设置页面标题的后缀
        this.titleService.suffix = appSettings.appInfo.name;
        // ACL：设置权限为全量
        this.aclService.setFull(false);
      },
        () => { },
        () => {
          // 初始化菜单
          const token = this.tokenService.get();
          if (token.token) {
            this.treeMenuService.getMenus().subscribe((allMenus: any) => {
              this.menuService.add(allMenus);
              resolve(null);
            });
            this.objAclService.queryAclNames().subscribe((acls: any) => {
              this.aclService.setAbility(acls);
              resolve(null);
            });
          } else {
            resolve(null);
          }
        });
  }

  async load(): Promise<any> {
    // only works with promises
    // https://github.com/angular/angular/issues/15088
    return new Promise((resolve, reject) => {
      this.setApp(resolve, reject);
    });
  }
}
