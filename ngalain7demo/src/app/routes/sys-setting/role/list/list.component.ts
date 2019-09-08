import { Component, OnInit, ChangeDetectorRef, ViewChild, Injectable } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { RoleUserTableComponent } from '../user-table/user-table.component';
import { RoleTableComponent } from '../role-table/role-table.component';


@Component({
  selector: 'app-sys-role-list',
  templateUrl: './list.component.html',
  providers: []
})
export class RoleListComponent implements OnInit {
  roleInfo: any;

  @ViewChild('roleTable') roleTable: RoleTableComponent;
  @ViewChild('roleUserTable') roleUserTable: RoleUserTableComponent;

  choosed(role: any): void {
    this.roleInfo = role;
  }

  ngOnInit(): void {

  }

}
