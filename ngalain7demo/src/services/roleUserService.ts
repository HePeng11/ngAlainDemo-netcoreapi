import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';
import { http_null_param } from './consts';


@Injectable()
export class RoleUserService {
  /**查询已绑定的用户 */
  query(pageIndex: number = 1, pageSize: number = 10, roleId: number, key: string): Observable<any> {
    let params = new HttpParams().
      append('pageindex', `${pageIndex}`)
      .append('pagesize', `${pageSize}`);
    if (roleId) { params = params.append('roleId', `${roleId}`); }
    if (key) { params = params.append('key', `${key}`); }
    return this.http.get(webApiUrls.roleUsers.query, { params });
  }

  /**查询该角色未绑定的用户 */
  QueryIsNotRole(pageIndex: number = 1, pageSize: number = 10, roleId: number, key: string, gender: string): Observable<any> {
    let params = new HttpParams().
      append('pageindex', `${pageIndex}`)
      .append('pagesize', `${pageSize}`);
    if (roleId) { params = params.append('roleId', `${roleId}`); }
    if (key) { params = params.append('key', `${key}`); }
    if (gender && gender !== http_null_param) { params = params.append('gender', `${gender}`); }
    return this.http.get(webApiUrls.roleUsers.queryNotbind, { params });
  }

  update(role: any): Observable<any> {
    return this.http.put(format(webApiUrls.roleUsers.update, { id: role.id }), role);
  }

  add(roles: any): Observable<any> {
    return this.http.post(webApiUrls.roleUsers.add, roles);
  }

  delete(ids: number[]): Observable<any> {
    return this.http.put(webApiUrls.roleUsers.delete, ids);
  }


  constructor(private http: HttpClient) { }


}
