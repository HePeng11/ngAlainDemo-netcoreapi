import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';

@Injectable()
export class TreeMenuService {

  getMenus(): Observable<any> {
    return this.http.get(webApiUrls.menus.getMenus);
  }

  getMenusExcept(id: number): Observable<any> {
    const params = new HttpParams().append('id', `${id}`);
    return this.http.get(webApiUrls.menus.getMenusExcept, { params });
  }

  queryMenus(pageIndex: number = 1, pageSize: number = 10): Observable<any> {
    const params = new HttpParams().append('pageindex', `${pageIndex}`).append('pagesize', `${pageSize}`);
    return this.http.get(webApiUrls.menus.query, { params });
  }

  updateMenu(menu: any): Observable<any> {
    return this.http.put(format(webApiUrls.menus.update, { id: menu.id }), menu);
  }

  addMenu(menu: any): Observable<any> {
    return this.http.post(webApiUrls.menus.add, menu);
  }


  deleteMenu(menu: any): Observable<any> {
    return this.http.delete(format(webApiUrls.menus.delete, { id: menu.id }));
  }

  constructor(private http: HttpClient) { }
}


export interface TreeNodeInterface {
  key: number;
  name: string;
  age: number;
  level: number;
  expand: boolean;
  address: string;
  children?: TreeNodeInterface[];
}
