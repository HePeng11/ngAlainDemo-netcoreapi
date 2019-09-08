import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExamplesListDemo1Component } from './list-demo1/list-demo1.component';
import { ExamplesListDemo2Component } from './list-demo2/list-demo2.component';

const routes: Routes = [

  { path: '', component: ExamplesListDemo1Component },
  { path: 'listDemo2', component: ExamplesListDemo2Component }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExamplesRoutingModule { }
