import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';


@Injectable()
export class RoleService {
  query(pageIndex: number = 1, pageSize: number = 10, key: string): Observable<any> {
    let params = new HttpParams().
      append('pageindex', `${pageIndex}`)
      .append('pagesize', `${pageSize}`);
    if (key) { params = params.append('key', `${key}`); }
    return this.http.get(webApiUrls.roles.query, { params });
  }

  update(role: any): Observable<any> {
    return this.http.put(format(webApiUrls.roles.update, { id: role.id }), role);
  }

  add(role: any): Observable<any> {
    return this.http.post(webApiUrls.roles.add, role);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(format(webApiUrls.roles.delete, { id: id }));
  }


  constructor(private http: HttpClient) { }



}
