import { NgModule } from '@angular/core';
import { DelonFormModule, WidgetRegistry } from '@delon/form';
import { SharedModule } from '@shared';
import { SelectWithIconWidgetComponent } from '@shared/Components/SelectWithIconWidgetComponent';
import { TreeSelectWidgetComponent } from '@shared/Components/TreeSelectWidgetComponent';
import { SelecIconWindowWidgetComponent } from '@shared/Components/SelecIconWindowWidgetComponent';
import { UploadUserHeadImgWidgetComponent } from '@shared/Components/UploadUserHeadImgWidgetComponent';
import { IconWindowComponent } from '@shared/Components/components/iconwindow/iconwindow.component';

// import { UEditorWidget } from './widgets/ueditor/ueditor.widget';

export const SCHEMA_THIRDS_COMPONENTS = [
  SelectWithIconWidgetComponent,
  TreeSelectWidgetComponent,
  SelecIconWindowWidgetComponent,
  IconWindowComponent,
  UploadUserHeadImgWidgetComponent
  // UEditorWidget
];

@NgModule({
  entryComponents: SCHEMA_THIRDS_COMPONENTS,
  declarations: SCHEMA_THIRDS_COMPONENTS,
  imports: [SharedModule, DelonFormModule.forRoot()],
  exports: SCHEMA_THIRDS_COMPONENTS,
})
export class JsonSchemaModule {
  constructor(widgetRegistry: WidgetRegistry) {
    widgetRegistry.register(SelecIconWindowWidgetComponent.KEY, SelecIconWindowWidgetComponent);
    widgetRegistry.register(UploadUserHeadImgWidgetComponent.KEY, UploadUserHeadImgWidgetComponent);
    widgetRegistry.register(SelectWithIconWidgetComponent.KEY, SelectWithIconWidgetComponent); // 暂未使用
    widgetRegistry.register(TreeSelectWidgetComponent.KEY, TreeSelectWidgetComponent); // 未使用
    // widgetRegistry.register(UEditorWidget.KEY, UEditorWidget);
  }
}
