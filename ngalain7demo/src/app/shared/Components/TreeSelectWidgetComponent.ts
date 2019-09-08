import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { ControlWidget } from '@delon/form';
import { NzTreeSelectComponent } from 'ng-zorro-antd';
import { identifierModuleUrl } from '@angular/compiler';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-custom-tree-select',
  template: `
  <sf-item-wrap  [schema]="schema" [ui]="ui" [showError]="showError" [error]="error" [showTitle]="schema.title">
    <nz-tree-select #dfad
      [nzDefaultExpandedKeys]="expandKeys"
      [nzNodes]="nodes"
      nzShowSearch
      nzPlaceHolder="Please select"
      [(ngModel)]="value"
      (ngModelChange)="onChange($event)"
    >
    </nz-tree-select>
    </sf-item-wrap>
  `
})
export class TreeSelectWidgetComponent extends ControlWidget implements OnInit, AfterViewInit {
  static readonly KEY = 'TreeSelectWidget';
  @ViewChild('dfad') treeSel: NzTreeSelectComponent;
  expandKeys = [];
  value: string;
  nodes = [];
  //   [{
  //     title: 'parent 1',
  //     key: '100',
  //     children: [
  //       {
  //         title: 'parent 1-0',
  //         key: '1001',
  //         children: [
  //           { title: 'leaf 1-0-0', key: '10010', isLeaf: true },
  //           { title: 'leaf 1-0-1', key: '10011', isLeaf: true }
  //         ]
  //       },
  //       {
  //         title: 'parent 1-1',
  //         key: '1002',
  //         children: [{ title: 'leaf 1-1-0', key: '10020', isLeaf: true }]
  //       }
  //     ]
  //   }
  // ];

  onChange($event: string): void {
    console.log($event);
  }

  ngOnInit(): void {
    this.ui.data(this.nodes);
  }

  ngAfterViewInit(): void {

  }

}
