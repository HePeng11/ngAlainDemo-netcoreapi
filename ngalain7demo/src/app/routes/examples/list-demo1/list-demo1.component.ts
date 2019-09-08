import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { STColumn, STComponent, STPage, STChange, STData, STRequestOptions } from '@delon/abc';
import { SFSchema, SFComponent } from '@delon/form';
import { debug } from 'util';
import { RtlScrollAxisType } from '@angular/cdk/platform';
import { text } from '@angular/core/src/render3';
import { toarry } from '@shared';
import { Data } from '@angular/router';

@Component({
  selector: 'app-examples-list-demo1',
  templateUrl: './list-demo1.component.html',
  styles: [
    `
      .operate {
        margin-bottom: 16px;
      }

      .operate span {
        margin-left: 8px;
      }
    `
  ]
})
export class ExamplesListDemo1Component implements OnInit {
  isAllDisplayDataChecked = false;
  isOperating = false;
  isIndeterminate = false;
  listOfDisplayData: Data[] = [];
  listOfAllData: Data[] = [];
  mapOfCheckedId: { [key: string]: boolean } = {};
  numberOfChecked = 0;
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
  currentPageDataChange($event: Data[]): void {
    this.listOfDisplayData = $event;
    this.refreshStatus();
  }

  refreshStatus(): void {
    this.isAllDisplayDataChecked = this.listOfDisplayData
      .filter(item => !item.disabled)
      .every(item => this.mapOfCheckedId[item.id]);
    this.isIndeterminate =
      this.listOfDisplayData.filter(item => !item.disabled).some(item => this.mapOfCheckedId[item.id]) &&
      !this.isAllDisplayDataChecked;
    this.numberOfChecked = this.listOfAllData.filter(item => this.mapOfCheckedId[item.id]).length;
  }

  checkAll(value: boolean): void {
    this.listOfDisplayData.filter(item => !item.disabled).forEach(item => (this.mapOfCheckedId[item.id] = value));
    this.refreshStatus();
  }

  operateData(): void {
    this.isOperating = true;
    // setTimeout(() => {
      this.listOfAllData.forEach(item => (this.mapOfCheckedId[item.id] = false));
      this.refreshStatus();
      this.isOperating = false;
    // }, 1000);
  }

  ngOnInit(): void {
    for (let i = 0; i < 100; i++) {
      this.listOfAllData.push({
        id: i,
        name: `Edward King ${i}`,
        age: 32,
        address: `London, Park Lane no. ${i}`,
        disabled: i % 2 === 0
      });
    }
  }
}
