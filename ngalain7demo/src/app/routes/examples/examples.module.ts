import { NgModule } from '@angular/core';
import { SharedModule } from '@shared';
import { ExamplesRoutingModule } from './examples-routing.module';
import { ExamplesListDemo1Component } from './list-demo1/list-demo1.component';
import { ExamplesListDemo2Component } from './list-demo2/list-demo2.component';

const COMPONENTS = [
  ExamplesListDemo1Component,
  ExamplesListDemo2Component];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [
    SharedModule,
    ExamplesRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class ExamplesModule { }
